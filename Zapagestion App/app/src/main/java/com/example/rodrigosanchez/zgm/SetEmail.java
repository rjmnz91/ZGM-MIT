package com.example.rodrigosanchez.zgm;

import java.util.ArrayList;
import java.util.concurrent.TimeoutException;


import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

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
import com.mitec.utilities.Utilerias;

public class SetEmail extends AppCompatActivity implements OnClickListener,
        MitControllerListener {

    private Button btn_enviar;
    private EditText txt_mail;
    private MitController mitController;
    private String folio;
    private BeanData data;
    private String amount, tarjeta, tipoMit, merchant, email;;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_set_email);
        init();

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }

        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));
    }

    @Override
    public void onBackPressed() {
        finish();
        android.os.Process.killProcess(android.os.Process.myPid());
        Intent intent = new Intent(SetEmail.this, Main.class);
        startActivity(intent);
    }

    @Override
    protected void onDestroy(){
        super.onDestroy();

        android.os.Process.killProcess(android.os.Process.myPid());
    }

    private void init() {
        btn_enviar = (Button) findViewById(R.id.btn_enviar);
        txt_mail = (EditText) findViewById(R.id.txt_mail);
        btn_enviar.setOnClickListener(this);
        mitController = new MitController(SetEmail.this);
        data = new BeanData();
        Intent intent = getIntent();

        if (intent != null) {
            folio = intent.getStringExtra("folio");
            amount = intent.getStringExtra("amount");
            tarjeta = intent.getStringExtra("tarjeta");
            tipoMit = intent.getStringExtra("tipoMit");
            merchant = intent.getStringExtra("merchant");
            email = intent.getStringExtra("correo");
        }

        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                txt_mail.setText(email);
            }
        });
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.btn_enviar:
                // folio = "000325384";
                String mail = txt_mail.getText().toString();

                if (mail != null && !mail.equalsIgnoreCase("")) {

                    try {
                        mitController.sndEmailWithAddress(mail, "", folio, data.getUser(),
                                data.getPwd(), data.getCompany(),
                                data.getBranch(), "MEX");
                    } catch (TimeoutException e) {
                        e.printStackTrace();
                    }

                } else {
                    // ALERT
                }
                break;
            default:
                break;
        }

    }

	/*@Override
	public void setMitError(String myError, int tipo) {
		// TODO Auto-generated method stub

	}*/

    @Override
    public void setResult(String Result) {

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setCancelable(false);
        builder.setTitle("Envío de correo");
        builder.setMessage("El comprobante ha sido enviado a " + txt_mail.getText().toString());
        builder.setInverseBackgroundForced(true);
        builder.setPositiveButton("Aceptar",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog,	int which) {
                        android.os.Process.killProcess(android.os.Process.myPid());
                        String newUrl = Common.getHomeURL()+"/FinalizaCompra.aspx$"+amount+"&"+tarjeta+"&"+tipoMit+"&"+merchant;
                        Common.setURL(newUrl);
                        Intent intent = new Intent(SetEmail.this, Main.class);
                        startActivity(intent);
                        finish();
                    }
                });

        AlertDialog alert = builder.create();
        alert.show();

    }



    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {
        // TODO Auto-generated method stub

    }

    @Override
    public void setMitError(MitError obj) {
        // TODO Auto-generated method stub

    }

    @Override
    public void setResult(com.mitec.beans.BeanResponseSell beanResponseSell,
                          String idMitTransaction) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishTransactionWithMerchant(ArrayList<BeanMerchant> bean) {
        // TODO Auto-generated method stub

    }

    @Override
    public void didFinishTransactionWithMerchantPDC(
            ArrayList<BeanMerchant> contado, ArrayList<BeanMerchant> msi,
            ArrayList<BeanMerchant> mci, String error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onRequestTextInfo(String msg) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onRequestTextInfo(String code, String msg) {

    }

    @Override
    public void onReturnWalkerUid(String uid) {
        // TODO Auto-generated method stub

    }

    @Override
    public void getWalkerBatteryLevel(String level) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onDeviceUnplugged(String msg) {
        // TODO Auto-generated method stub

    }

    @Override
    public void getTransaction(ArrayList<com.mitec.beans.BeanResponseSell> list) {
        // TODO Auto-generated method stub

    }

	/*@Override
	public void setMitError(String message, MitError obj) {
		// TODO Auto-generated method stub
		System.out.println("setMitError " + message);

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
