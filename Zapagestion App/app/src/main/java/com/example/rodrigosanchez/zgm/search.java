package com.example.rodrigosanchez.zgm;

import java.util.ArrayList;
import java.util.concurrent.TimeoutException;

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

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.text.style.RelativeSizeSpan;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

public class search extends AppCompatActivity implements OnClickListener, MitControllerListener{

    /* View variables*/
    private EditText editReference;
    private EditText editDate;
    private Button buttonSearh;
    private ProgressDialog progressDialog;

    /* Search variables*/
    private String user;
    private String password;
    private String company;
    private String date;
    private String branch;
    private String reference;

    /* MitController variables*/
    private MitController mitController;
    private BeanData beanData;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));

        init();
    }

    private void init(){

        editReference = (EditText)findViewById(R.id.editReference);
        editDate = (EditText)findViewById(R.id.editDate);
        buttonSearh = (Button)findViewById(R.id.buttonSearch);
        buttonSearh.setOnClickListener(this);

        beanData = new BeanData();
        mitController = new MitController(search.this);
        mitController.setURL(beanData.getServer());

        user = beanData.getUser();
        password = beanData.getPwd();
        company = beanData.getCompany();
        branch = beanData.getBranch();

        //editDate.setText("07/05/16");
        //editReference.setText("CALOR");
    }

    @Override
    public void onClick(View v) {

        date = editDate.getText().toString();
        reference = editReference.getText().toString();

        if(v.getId() == buttonSearh.getId()){
            if(editDate.getText().toString().equalsIgnoreCase("") || editReference.getText().toString().equalsIgnoreCase("")){
                messageBox("Error", "Campos vacios", "Aceptar", search.this);
            }
            else{

                this.runOnUiThread(new Runnable() {
                    public void run() {
                        progressDialog = new ProgressDialog(search.this);
                        SpannableString ss1=  new SpannableString("Búsqueda en progreso");
                        ss1.setSpan(new RelativeSizeSpan(1f), 0, ss1.length(), 0);
                        ss1.setSpan(new ForegroundColorSpan(Color.parseColor("#000062")), 0, ss1.length(), 0);
                        SpannableString ss2=  new SpannableString("Buscando transacción...");
                        ss2.setSpan(new RelativeSizeSpan(1f), 0, ss2.length(), 0);
                        ss2.setSpan(new ForegroundColorSpan(Color.parseColor("#0266a9")), 0, ss2.length(), 0);
                        progressDialog.setTitle(ss1);
                        progressDialog.setMessage(ss2);
                        progressDialog.setCancelable(false);

                        progressDialog.show();
                    }
                });

                new SearchTransaction().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
            }

        }

    }

    public void messageBox(final String titulo, final String mensaje, final String msj_boton,
                           final Activity activity) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                AlertDialog.Builder alertDialog;
                alertDialog = new AlertDialog.Builder(activity);
                alertDialog.setCancelable(false);
                alertDialog.setTitle(titulo);
                alertDialog.setMessage(mensaje);
                alertDialog.setPositiveButton(msj_boton, null);
                alertDialog.show();
            }
        });

    }

    @Override
    public void onBackPressed(){
        finish();

    }

    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {
        // TODO Auto-generated method stub

    }

    @Override
    public void setMitError(MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void setResult(String Result) {
        // TODO Auto-generated method stub

    }

    @Override
    public void setResult(BeanResponseSell beanResponseSell,
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
        // TODO Auto-generated method stub

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
    public void getTransaction(ArrayList<BeanResponseSell> list) {
        // TODO Auto-generated method stub

    }

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
    public void onReturnPnrInformation(MITPnrInformation pnrInformation,
                                       MitError error) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onReturnLastTransaction(BeanResponseSell beanResponseSell,	MitError error) {

        if(progressDialog != null)
            progressDialog.dismiss();

        if(beanResponseSell != null){
            Intent intent = new Intent(search.this, Result.class);

            intent.putExtra("beanResponseSell", beanResponseSell);
            intent.putExtra("isSearch", true);

            if(beanResponseSell.getResponse().equals("denied")){
                if(beanResponseSell.getDescription().equals(""))
                    intent.putExtra("error", "Transacción declinada");
                else
                    intent.putExtra("error", beanResponseSell.getDescription());
            }
            else if(beanResponseSell.getResponse().equals("error")){
                if(beanResponseSell.getDescription().equals(""))
                    intent.putExtra("error", "Error en la transacción");
                else
                    intent.putExtra("error", beanResponseSell.getDescription());

            }
            finish();
            startActivity(intent);

        }
        else if (error != null){
            System.out.println(" error.getDescripcion() " +  error.getDescripcion());
            messageBox("Error en la búsqueda", error.getDescripcion(), "Aceptar", search.this);
        }

    }

    private class SearchTransaction extends AsyncTask<Void, Void, Void> {

        @Override
        protected void onPreExecute() {

        }

        @Override
        protected Void doInBackground(Void... arg0) {
            try {
                mitController.checkTransactionWithUser(user, password, company, date, branch, reference);
            } catch (TimeoutException e) {
                messageBox("TimeOut", "Error de conexión, favor de intentar de nuevo", "Aceptar", search.this);
                e.printStackTrace();
            }
            return null;
        }
    }
    @Override
    protected void onPause(){
        super.onPause();
        if(progressDialog != null)
            progressDialog.dismiss();
    }

    @Override
    protected void onStop(){
        super.onStop();
        if(progressDialog != null)
            progressDialog.dismiss();
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
