package com.example.rodrigosanchez.zgm;

import java.util.ArrayList;
import java.util.concurrent.TimeoutException;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
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
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

import com.mitec.beans.BeanResponseSell;
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
import com.mitec.beans.EmvApplication;
import com.mitec.beans.MITPnrInformation;
import com.mitec.controller.MitController;
import com.mitec.controller.MitControllerListener;
import com.mitec.utilities.FingerPathView;
import com.mitec.utilities.Utilerias;

public class Signature extends AppCompatActivity implements OnClickListener,
        MitControllerListener {

    FingerPathView signView;
    Button btnClean;
    Button btn;
    TextView hint_text;
    MitController myController;
    String folio = "";
    Bitmap sign;
    String cad;
    Utilerias util = new Utilerias();
    BeanData data = new BeanData();
    ProgressDialog progressDialog;
    BeanResponseSell beanResponseSell;

    private TextView txt_msg;
    private Button btn_ok;
    private Dialog dialog;

    private String amount, tarjeta, tipoMit, merchant, email;

    String auth = "";

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

    void init() {
        signView = (FingerPathView) findViewById(R.id.sign_view);
        btnClean = (Button) findViewById(R.id.btn_clear);
        btn = (Button) findViewById(R.id.btn_ok);
        btnClean.setOnClickListener(this);
        progressDialog = new ProgressDialog(Signature.this);

        btn.setOnClickListener(this);
        myController = new MitController(Signature.this);
        myController.setURL(data.getServer());
        Intent intent = getIntent();
        beanResponseSell = (BeanResponseSell)intent.getSerializableExtra("beanResponseSell");

        if (intent != null) {
            cad = intent.getStringExtra("cad_sign");
            folio = intent.getStringExtra("folio");
            amount = intent.getStringExtra("amount");
            tarjeta = intent.getStringExtra("tarjeta");
            tipoMit = intent.getStringExtra("tipoMit");
            merchant = intent.getStringExtra("merchant");
            email = intent.getStringExtra("correo");
        }
        signView.init(cad, signView);
    }

    @SuppressLint("NewApi")
    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.btn_clear:
                signView.clearCanvas();
                break;
            case R.id.btn_ok:

                if (!signView.Empty()) {
                    if (Build.VERSION.SDK_INT <= Build.VERSION_CODES.GINGERBREAD_MR1) {
                        new procesando().execute();
                    } else {
                        progressDialog.setTitle("Firma tu comprobante");
                        progressDialog.setMessage("Enviando firma");
                        progressDialog.setCancelable(false);
                        progressDialog.show();
                        new procesando().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                    }
                } else {

                    txt_msg.setText("Es necesaria la firma para procesar la transacción");

                    btn_ok.setOnClickListener(new View.OnClickListener() {

                        @Override
                        public void onClick(View arg0) {
                            dialog.dismiss();
                        }
                    });
                    dialog.show();

                }
                break;
            default:
                break;
        }

    }

    @Override
    public void onBackPressed() {
    }

    public void MessageBox(final String titulo, final String mensaje, final String msj_boton,
                           final Activity activity) {

        Signature.this.runOnUiThread(new Runnable() {
            public void run() {

                AlertDialog.Builder alertDialog;
                alertDialog = new AlertDialog.Builder(activity);
                alertDialog.setTitle(titulo);
                alertDialog.setMessage(mensaje);
                alertDialog.setPositiveButton(msj_boton, null);
                alertDialog.show();
            }
        });
    }

	/*@Override
	public void setMitError(String myError, int tipo) {


	}*/

    @Override
    public void setResult(String Result) {
        progressDialog.dismiss();
        finish();
        Intent i = new Intent(Signature.this, SetEmail.class);
        i.putExtra("folio", folio);
        i.putExtra("amount",amount);
        i.putExtra("tarjeta",tarjeta);
        i.putExtra("tipoMit",tipoMit);
        i.putExtra("merchant",merchant);
        i.putExtra("correo",email);

        startActivity(i);

    }

    @Override
    public void setResult(BeanResponseSell beanResponseSell,
                          String idMitTransaction) {
    }

    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {

    }

    @Override
    public void onRequestTextInfo(String msg) {

    }

    @Override
    public void onRequestTextInfo(String code, String msg) {

    }

    @Override
    public void onReturnWalkerUid(String uid) {

    }

    @Override
    public void getWalkerBatteryLevel(String level) {

    }

    private class procesando extends AsyncTask<Void, Void, Void> {

        @Override
        protected void onPreExecute() {
        }

        @Override
        protected Void doInBackground(Void... arg0) {
            String image = signView.setCodeImage(findViewById(R.id.signView));
            try {
                myController.uploadSignWithImage(image, folio);
            } catch (TimeoutException e) {
                e.printStackTrace();
            }
            return null;

        }

    }

    @Override
    public void didFinishTransactionWithMerchant(ArrayList<BeanMerchant> bean) {

    }

    @Override
    public void onDeviceUnplugged(String msg) {

    }

    @Override
    public void setMitError(MitError obj) {
        progressDialog.dismiss();
        MessageBox("Error", obj.getDescripcion(), "Ok", Signature.this);
    }

    @Override
    public void getTransaction(ArrayList<BeanResponseSell> list) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishTransactionWithMerchantPDC(
            ArrayList<BeanMerchant> contado, ArrayList<BeanMerchant> msi,
            ArrayList<BeanMerchant> mci, String error) {
        // TODO Auto-generated method stub

    }

	/*@Override
	public void setMitError(String message, MitError obj) {
		// TODO Auto-generated method stub

	}*/

    @Override
    public void didFinishTransactionWithDccOption(DccOption localDccOption,
                                                  DccOption foreignOption) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishSendElectronicBill(String message, MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishSendSMS(MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onReturnPnrInformation(MITPnrInformation pnrInformation, MitError error){

    }

    @Override
    public void onReturnLastTransaction(BeanResponseSell beanResponseSell,
                                        MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishRewardTransaction(
            BeanRewardTransaction beanRewardTransaction, String idMitTransaction) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishRewardsReport(BeanRewardsReport beanRewardsReport) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onReturnCardNumber(String cardNumber) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onReturnPDInformation(
            ArrayList<BeanPDReferecenInfo> listPDReferenceInfo,
            BeanPDPaymentInfo PDPaymentInfo, MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishSodexoTransaction(BeanSodexoResponse sodexoResponse,
                                           String idmitTransaction) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onReturnEmvApplication(ArrayList<String> emvApplications){
        Utilerias.println("Llega al delegado de multiaplicación " + emvApplications.toString());
    }

}
