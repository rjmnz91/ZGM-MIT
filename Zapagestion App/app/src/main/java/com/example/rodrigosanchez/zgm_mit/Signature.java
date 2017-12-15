package com.example.rodrigosanchez.zgm_mit;

import java.util.ArrayList;
import java.util.concurrent.TimeoutException;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.mit.creditCardValidator.MITCardInformation;
import com.mit.error.MitError;
import com.mitec.beans.BeanMerchant;
import com.mitec.beans.BeanPDPaymentInfo;
import com.mitec.beans.BeanPDReferecenInfo;
import com.mitec.beans.BeanResponseSell;
import com.mitec.beans.BeanRewardTransaction;
import com.mitec.beans.BeanRewardsReport;
import com.mitec.beans.BeanSodexoResponse;
import com.mitec.beans.DccOption;
import com.mitec.beans.MITPnrInformation;
import com.mitec.controller.MitController;
import com.mitec.controller.MitControllerListener;
import com.mitec.utilities.FingerPathView;
import com.mitec.utilities.Utilerias;


public class Signature extends AppCompatActivity implements View.OnClickListener, MitControllerListener {

    FingerPathView signView;
    Button btnClean;
    Button btn;
    TextView hint_text;
    MitController myController;
    String folio = "", cad,s,email,correoComercio,user,password,company,branch,country,amount,tarjeta,tipoMit,merchant,auth,opcionSms,numero,carrier, image;

    Bitmap sign;
    Utilerias util = new Utilerias();
    BeanData data = new BeanData();
    ProgressDialog progressDialog;

    private TextView txt_msg;
    private Button btn_ok;
    private Dialog dialog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signature);

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        init();
        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));
    }

    public void init(){
        signView = (FingerPathView) findViewById(R.id.sign_view);
        btnClean = (Button) findViewById(R.id.btn_clear);
        btn = (Button) findViewById(R.id.btn_ok);
        btnClean.setOnClickListener(this);
        progressDialog = new ProgressDialog(Signature.this);
        btn.setOnClickListener(this);
        myController = new MitController(Signature.this);
        myController.setURL(data.getServer());
        Intent intent = getIntent();

        if (intent != null) {
            cad = intent.getStringExtra("cad_sign");
            folio = intent.getStringExtra("folio");
            email = intent.getStringExtra("email");
            correoComercio = intent.getStringExtra("correoComercio");
            user = intent.getStringExtra("user");
            password = intent.getStringExtra("password");
            company = intent.getStringExtra("company");
            branch = intent.getStringExtra("branch");
            country = intent.getStringExtra("country");
            amount = intent.getStringExtra("amount");
            tarjeta = intent.getStringExtra("tarjeta");
            tipoMit = intent.getStringExtra("tipoMit");
            merchant = intent.getStringExtra("merchant");
            auth = intent.getStringExtra("auth");
            opcionSms = intent.getStringExtra("sms");
            if(opcionSms.equals("1")) {
                numero = intent.getStringExtra("numero");
                carrier = intent.getStringExtra("carrier");
            }
            else{
            }
        }
        signView.init(cad, signView);
    }

    @SuppressLint("NewApi")
    @Override
    public void onClick(View view) {
        if(view.getId() == btnClean.getId()){
            signView.clearCanvas();
        /*}else if(view.getId() == btn_ok.getId()){
            if(!signView.Empty()){
                if (Build.VERSION.SDK_INT <= Build.VERSION_CODES.GINGERBREAD_MR1) {
                    new procesando().execute();
                } else {
                    progressDialog.setTitle("Firma tu comprobante");
                    progressDialog.setMessage("Enviando firma");
                    progressDialog.setCancelable(false);
                    progressDialog.show();
                    new procesando().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                }
            }else{
                Toast.makeText(getApplicationContext(),"Es necesaria la firma para procesar la transacción",Toast.LENGTH_LONG).show();
            }*/
        }else if(view.getId() == btn.getId()){
            if(!signView.Empty()){
                try {
                    if (Build.VERSION.SDK_INT <= Build.VERSION_CODES.GINGERBREAD_MR1) {
                        myController.uploadSignWithImage(image, folio);
                    } else {
                        progressDialog.setTitle("Firma tu comprobante");
                        progressDialog.setMessage("Enviando firma");
                        progressDialog.setCancelable(false);
                        progressDialog.show();
                        image = signView.setCodeImage(findViewById(R.id.sign_view));
                        myController.uploadSignWithImage(image,folio);
                        myController.sndEmailWithAddress(email,correoComercio,folio,user,password,company,branch,country);
                        String newUrl = Common.getHomeURL()+"/CarritoDetalle.aspx?"+amount+"$"+tarjeta+"$"+tipoMit+"$"+merchant+"$"+email+ "$" + auth + "$" + folio;
                        Common.setURL(newUrl);
                        if(opcionSms.equals("1")){
                            myController.sndSMS(numero,carrier,folio,user,password,company,branch);
                        }else{
                        }
                        finish();
                        ////////android.os.Process.killProcess(android.os.Process.myPid());
                        Intent i = new Intent(Signature.this, Zapagestion.class);
                        startActivity(i);
                    }
                }catch (TimeoutException te){

                }
            }else{
                Toast.makeText(getApplicationContext(),"Es necesaria la firma para procesar la transacción",Toast.LENGTH_LONG).show();
            }
        }
    }

    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {

    }

    @Override
    public void setMitError(MitError mitError) {

    }

    @Override
    public void setResult(String s) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setCancelable(false);
        builder.setTitle("Envío de correo");
        builder.setMessage("El comprobante ha sido enviado a " + email);
        String newUrl = Common.getHomeURL()+"/CarritoDetalle.aspx?"+amount+"$"+tarjeta+"$"+tipoMit+"$"+merchant+"$"+email+ "$" + auth + "$" + folio;
        Common.setURL(newUrl);
        builder.setInverseBackgroundForced(true);
        builder.setPositiveButton("Aceptar",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog,	int which) {
                        finish();
                        ////////android.os.Process.killProcess(android.os.Process.myPid());
                        Intent intent = new Intent(Signature.this, Zapagestion.class);
                        startActivity(intent);
                    }
                });

        AlertDialog alert = builder.create();
        alert.show();
    }

    @Override
    public void setResult(BeanResponseSell beanResponseSell, String s) {

    }

    @Override
    public void didFinishTransactionWithMerchant(ArrayList<BeanMerchant> arrayList) {

    }

    @Override
    public void didFinishTransactionWithMerchantPDC(ArrayList<BeanMerchant> arrayList, ArrayList<BeanMerchant> arrayList1, ArrayList<BeanMerchant> arrayList2, String s) {

    }

    @Override
    public void onRequestTextInfo(String s) {

    }

    @Override
    public void onRequestTextInfo(String s, String s1) {

    }

    @Override
    public void onReturnWalkerUid(String s) {

    }

    @Override
    public void getWalkerBatteryLevel(String s) {

    }

    @Override
    public void onDeviceUnplugged(String s) {

    }

    @Override
    public void getTransaction(ArrayList<BeanResponseSell> arrayList) {

    }

    @Override
    public void didFinishTransactionWithDccOption(DccOption dccOption, DccOption dccOption1) {

    }

    @Override
    public void didFinishSendElectronicBill(String s, MitError mitError) {

    }

    @Override
    public void didFinishSendSMS(MitError mitError) {

    }

    @Override
    public void onReturnPnrInformation(MITPnrInformation mitPnrInformation, MitError mitError) {

    }

    @Override
    public void onReturnEmvApplication(ArrayList<String> arrayList) {

    }

    @Override
    public void onReturnLastTransaction(BeanResponseSell beanResponseSell, MitError mitError) {

    }

    @Override
    public void didFinishRewardTransaction(BeanRewardTransaction beanRewardTransaction, String s) {

    }

    @Override
    public void didFinishRewardsReport(BeanRewardsReport beanRewardsReport) {

    }

    @Override
    public void onReturnCardNumber(String s) {

    }

    @Override
    public void onReturnPDInformation(ArrayList<BeanPDReferecenInfo> arrayList, BeanPDPaymentInfo beanPDPaymentInfo, MitError mitError) {

    }

    @Override
    public void didFinishSodexoTransaction(BeanSodexoResponse beanSodexoResponse, String s) {

    }

}