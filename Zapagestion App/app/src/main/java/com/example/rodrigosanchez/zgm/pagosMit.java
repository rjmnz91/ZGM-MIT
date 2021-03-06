package com.example.rodrigosanchez.zgm;

import android.app.Activity;
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
import android.support.annotation.RequiresApi;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;
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
import com.mitec.controller.PaymentDevice;
import com.mitec.utilities.FingerPathView;
import com.mitec.utilities.Utilerias;

import java.util.ArrayList;
import java.util.Random;
import java.util.concurrent.TimeoutException;

import com.example.rodrigosanchez.zgm.R;
import com.example.rodrigosanchez.zgm.BeanData;


public class pagosMit extends AppCompatActivity implements View.OnClickListener,  MitControllerListener, AdapterView.OnItemSelectedListener{

    private EditText txt_amount;
    //private Spinner spn_merchant;
    private Spinner spn_propina;
    private Spinner spn_plazo;
    private TextView totalPropina;
    private TextView labelPropina;
    private EditText txt_reference;
    private Button btn_start;
    private Button btn_cancel;
    private Button btn_Conect;
    private ArrayList<String> list;
    private ArrayList<String> listPropina;
    private String reader;
    String plazo = "";
    private MitController myController;
    private BeanData data;
    private ProgressDialog progressDialog;
    private String image;
    private String amount;
    private String merchant;
    private String company;
    private String branch;
    private String user;
    private String password;
    private String usrTrx;
    private String refPay;
    private String operationType;
    private String country;
    private String currency;
    private String accion;
    private String cuarto;
    private String mesa;
    private String mesero;
    private String turno;
    private String email;
    private String tarjeta;
    private String tipoMit;
    private String auth;
    private String operation;
    private Button btn_ok;
    private TextView txtFirma;
    String months = "";
    String Device = "";
    String correoComercio = "";
    DatabaseHandler dbh;
    FingerPathView signView;
    Button btnClean;
    Button btn;
    TextView hint_text;
    String folio = "";
    Bitmap sign;
    String cad;
    Utilerias util = new Utilerias();

    private TextView labelConnected;
    private boolean isDeviceSelected;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pagos_mit);

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        init();

        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));
    }

    private void init() {
        dbh = new DatabaseHandler(getApplicationContext());
        data = new BeanData();
        signView = (FingerPathView) findViewById(R.id.sign_view);
        btn = (Button) findViewById(R.id.btn_ok);
        btn.setOnClickListener(this);
        btn_Conect = (Button) findViewById(R.id.btnConect);
        btn_Conect.setOnClickListener(this);
        progressDialog = new ProgressDialog(this);
        txt_amount = (EditText) findViewById(R.id.txt_amount);
        //spn_merchant = (Spinner) findViewById(R.id.spn_merchant);
        spn_plazo = (Spinner) findViewById(R.id.spn_plazo);
        txt_reference = (EditText) findViewById(R.id.txt_reference);
        txtFirma = (TextView) findViewById(R.id.txtFirma);
        btn_start = (Button) findViewById(R.id.btn_start);
        btn_cancel = (Button) findViewById(R.id.btn_cancel);

        myController = new MitController(pagosMit.this);
        myController.setURL(data.getServer());
        btn_start.setOnClickListener(this);
        btn_cancel.setOnClickListener(this);

        labelConnected = (TextView)findViewById(R.id.labelConnected);


        spn_plazo.setOnItemSelectedListener(this);


        company = data.getCompany();
        branch = data.getBranch();
        user = data.getUser();
        password = data.getPwd();
        operationType = "32";
        country = "MEX";
        currency = "MXN";

        accion = "0";

        //initReader();

        Intent intent = getIntent();
        if (intent != null) {
            refPay = intent.getStringExtra("vtaRef");
            amount = intent.getStringExtra("ttlPago");
            email = intent.getStringExtra("correo");
            accion = intent.getStringExtra("accion");
            cuarto = intent.getStringExtra("cuarto");
            mesa = intent.getStringExtra("mesa");
            mesero = intent.getStringExtra("mesero");
            turno = intent.getStringExtra("turno");
        }

        correoComercio = dbh.getCorreo();
        Device = dbh.getDevice();

        isDeviceSelected = false;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                String qty = amount.replace("%20","");
                qty = qty.replace(",","");
                txt_amount.setText(qty);
                txt_reference.setText(refPay);
            }
        });


    }

    private void initSignature(BeanResponseSell beanResponseSell, String idMitTransaction){
        cad = idMitTransaction;
        folio = beanResponseSell.getFoliocpagos();
        signView.init(cad, signView);
    }

    @Override
    protected void onStart(){
        super.onStart();
        //txt_amount.setText("10");
        //txt_reference.setText("referencia");
    }

    @Override
    protected void onResume(){
        super.onResume();

        //validationTypeReader();
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    @Override
    public void onBackPressed(){
        myController.deviceDissconect();
        finish();
        android.os.Process.killProcess(android.os.Process.myPid());
    }

    @Override
    protected void onDestroy(){
        super.onDestroy();
        System.out.println("Se destruye venta");
        if(Device.equals("VX600"))
            myController.deviceDissconect();

        //else
        //myController.stopService();

        if(progressDialog != null)
            progressDialog.dismiss();
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    @Override
    public void onClick(View view) {

        if(view.getId() == btn_start.getId()){
            if(!txt_amount.getText().toString().equals("") && !txt_reference.getText().toString().equals("")){
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        txt_amount.setEnabled(false);
                        txt_reference.setEnabled(false);
                        spn_plazo.setEnabled(false);
                        //spn_merchant.setEnabled(false);
                        btn_start.setEnabled(false);
                        btn_cancel.setEnabled(false);
                        btn_start.setBackgroundColor(Color.parseColor("#c5c6c9"));
                        btn_cancel.setBackgroundColor(Color.parseColor("#c5c6c9"));
                    }
                });
                if(Device.equals("VX600")){
                    setPaymentMode();
                }
                else{
                    if(Device.equals("NomadWP2") || Device.equals("QPOS BT") || Device.equalsIgnoreCase("Walker BT")){
                        if(isDeviceSelected)
                            sndProcess();
                        else
                            new ConnectDevice().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                    }
                    else{
                        sndProcess();
                    }
                    //myController.getDeviceInfo();
                }
            }
            else
                Toast.makeText(pagosMit.this, "Campos vacios", Toast.LENGTH_LONG).show();
        }else if(view.getId() == btn_cancel.getId()) {
            String newUrl = Common.getHomeURL()+"/Inicio.aspx";
            Common.setURL(newUrl);
            Intent intent = new Intent(pagosMit.this, Main.class);
            setResult(Activity.RESULT_OK, intent);
            myController.deviceDissconect();
            myController = null;
            finish();
        }else if(view.getId() == btn_Conect.getId()){
            if(Device.equals("QPOS BT")){
                myController.setDevice(PaymentDevice.QPOS_BT);
                new ConnectDevice().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        btn_start.setEnabled(true);
                        btn_start.setBackgroundResource(android.R.color.holo_green_dark);
                        btn_Conect.setVisibility(View.INVISIBLE);
                    }
                });
            }
            else if(Device.equalsIgnoreCase("Walker BT")){
                myController.setDevice(PaymentDevice.WALKER_BT);
                new ConnectDevice().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        btn_start.setEnabled(true);
                        btn_start.setBackgroundResource(android.R.color.holo_green_dark);
                        btn_Conect.setVisibility(View.INVISIBLE);
                    }
                });
            }
        }else if(view.getId() == btn.getId()){
            if (!signView.Empty()) {
                try {
                    if (Build.VERSION.SDK_INT <= Build.VERSION_CODES.GINGERBREAD_MR1) {
                        myController.uploadSignWithImage(image, folio);
                    } else {
                        progressDialog.setTitle("Firma tu comprante");
                        progressDialog.setMessage("Enviando firma");
                        progressDialog.setCancelable(false);
                        progressDialog.show();
                        image = signView.setCodeImage(findViewById(R.id.sign_view));
                        myController.uploadSignWithImage(image, folio);
                        myController.sndEmailWithAddress(email, correoComercio, folio, data.getUser(),
                                data.getPwd(), data.getCompany(),
                                data.getBranch(), "MEX");
                        //new procesando().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                        String newUrl = Common.getHomeURL()+"/CarritoDetalle.aspx?"+amount+"$"+tarjeta+"$"+tipoMit+"$"+
                                merchant+"$"+email+ "$" + auth + "$" + operation;
                        Common.setURL(newUrl);
                        Intent intent = new Intent(pagosMit.this, Main.class);
                        setResult(Activity.RESULT_OK, intent);
                        myController.deviceDissconect();
                        myController = null;
                        finish();
                    }
                }catch (TimeoutException te){}
                catch (Exception e){}
            } else {
                Toast.makeText(getApplicationContext(),"Es necesaria la firma para procesar la transacción",Toast.LENGTH_LONG).show();
            }

        }
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
            plazo = adapterView.getSelectedItem().toString();
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }

    private class procesando extends AsyncTask<Void, Void, Void> {

        @Override
        protected void onPreExecute() {
        }

        @Override
        protected Void doInBackground(Void... arg0) {
            return null;
        }

    }


    private void setPaymentMode(){
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(pagosMit.this);
        alertDialogBuilder.setTitle("");

        alertDialogBuilder.setMessage("?Como desea procesar su transacci?n?")
                .setCancelable(true)
                .setPositiveButton("Chip", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        progressDialog.setTitle("Espere un momento");
                        progressDialog.setMessage("Procesando...");
                        progressDialog.show();
                        myController.setPaymentMode("chip");
                        sndProcess();
                    }
                })

                .setNegativeButton("Banda", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        progressDialog.setTitle("Espere un momento");
                        progressDialog.setMessage("Procesando...");
                        progressDialog.show();
                        myController.setPaymentMode("swipe");
                        sndProcess();
                    }
                });

        AlertDialog alertDialog = alertDialogBuilder.create();
        alertDialog.show();
    }

    private void sndProcess(){
        progressDialog.setMessage("Procesando");
        progressDialog.setTitle("Espere un momento");
        progressDialog.show();
        Random random = new Random();
        int randomNum = random.nextInt((1000 - 0) + 1) + 0;
        amount = txt_amount.getText().toString();
        usrTrx = user;
        refPay = txt_reference.getText().toString().trim(); //+ randomNum;

		/*amount = txt_amount.getText().toString();
		usrTrx = user;
		refPay = txt_reference.getText().toString().trim();*/
        //myController.setReset(true);
        if(plazo!="")
            if(plazo.contains("3M")) {
                merchant = "157490";
            }else if(plazo.contains("6M")) {
                merchant = "157491";
            }else
                merchant = data.getMerchant();
        myController.sndEmvDirectSellWithAmount(amount, company, branch,user, password, usrTrx, merchant, refPay, operationType,country, currency, "");

        //myController.getDeviceInfo();
    }

    // **************************************************************

    public void MessageBox(final String titulo, final String mensaje, final String msj_boton,
                           final Activity activity) {
        runOnUiThread(new Runnable() {
            @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
            @Override
            public void run() {
		    	/*AlertDialog.Builder alertDialog;
		 		alertDialog = new AlertDialog.Builder(activity);
		 		alertDialog.setTitle(titulo);
		 		alertDialog.setMessage(mensaje);
		 		alertDialog.setPositiveButton(msj_boton, null);
		 		alertDialog.show();*/

                //finish();
                //startActivity(new Intent(pagosMit.this, Result.class).putExtra("error",mensaje));

                String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?"+mensaje ;
                Common.setURL(newUrl);

                Intent intent = new Intent(pagosMit.this, Main.class);
                setResult(Activity.RESULT_OK, intent);
                myController.deviceDissconect();
                myController = null;
                finish();
            }
        });

    }

	/*@Override
	public void setMitError(String myError, int tipo) {

	}*/

    @Override
    public void setResult(String result) {
        progressDialog.dismiss();
        if(result.equalsIgnoreCase("Desconectado")){
            labelConnected.setTextColor(Color.parseColor("#e60000"));
        }
        else{
            labelConnected.setTextColor(Color.parseColor("#8DC63F"));
        }

        labelConnected.setText(result);
    }

    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {

        //System.out.println("isFinishedCardReader");
        //System.out.println("sndPay()");
        myController.sndPay(merchant);
    }

    @Override
    public void setMitError(final MitError obj) {
        System.out.println("setMitError " + obj.getDescripcion());
        pagosMit.this.runOnUiThread(new Runnable() {
            public void run() {
                System.out.println("setMit error obj " + obj.getDescripcion());
                progressDialog.dismiss();
                MessageBox("Error", obj.getDescripcion(), "Ok", pagosMit.this);
            }
        });
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    @Override
    public void setResult(BeanResponseSell beanResponseSell, String idMitTransaction) {

        //System.out.println("setResult " + beanResponseSell.getCp());
        progressDialog.dismiss();
        initSignature(beanResponseSell,idMitTransaction);
        //Toast.makeText(this, "Termin? la transacci?n " + beanResponseSell.getResponse(), Toast.LENGTH_LONG).show();

        if(beanResponseSell.getResponse().equals("approved")){
            progressDialog.dismiss();
            auth = beanResponseSell.getAuth();
            operation = beanResponseSell.getFoliocpagos();
            /**/
            String response = "Response " +  beanResponseSell.getResponse() + "\n";
            response += "Folio " +  beanResponseSell.getFoliocpagos() + "\n";
            response += "Date " +  beanResponseSell.getDate() + "\n";
            response += "Time " +  beanResponseSell.getTime() + "\n";
            response += "Auth " +  beanResponseSell.getAuth() + "\n";

            if(beanResponseSell.getCp() != null && beanResponseSell.getCp().equals("1")) {
                try {
                    myController.sndEmailWithAddress(email, correoComercio, beanResponseSell.getFoliocpagos(), data.getUser(),
                            data.getPwd(), data.getCompany(),
                            data.getBranch(), "MEX");
                } catch (TimeoutException te) {
                    te.printStackTrace();
                }
                String newUrl = Common.getHomeURL() + "/CarritoDetalle.aspx?" + amount + "$" + beanResponseSell.getCc_number() + "$" + beanResponseSell.getAppidlabel()
                        + "$" + merchant + "$" + email + "$" + beanResponseSell.getAuth() + "$" + beanResponseSell.getFoliocpagos();
                Common.setURL(newUrl);
                Intent intent = new Intent(pagosMit.this, Main.class);
                setResult(Activity.RESULT_OK, intent);
                myController.deviceDissconect();
                myController = null;
                finish();
            }
            else {
                if(beanResponseSell.getStQPS().equals("1")){
                    try {
                        myController.sndEmailWithAddress(email, correoComercio, beanResponseSell.getFoliocpagos(), data.getUser(),
                                data.getPwd(), data.getCompany(),
                                data.getBranch(), "MEX");
                    } catch (TimeoutException te) {
                        te.printStackTrace();
                    }
                    String newUrl = Common.getHomeURL() + "/CarritoDetalle.aspx?" + amount + "$" + beanResponseSell.getCc_number() + "$" + beanResponseSell.getAppidlabel()
                            + "$" + merchant + "$" + email + "$" + beanResponseSell.getAuth() + "$" + beanResponseSell.getFoliocpagos();
                    Common.setURL(newUrl);
                    myController.setReset(true);
                    Intent intent = new Intent(pagosMit.this, Main.class);
                    setResult(Activity.RESULT_OK, intent);
                    myController.deviceDissconect();             finish();
                }
                else {
                    tarjeta = beanResponseSell.getCc_number();
                    tipoMit = beanResponseSell.getAppidlabel();
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            btn.setVisibility(View.VISIBLE);
                            signView.setVisibility(View.VISIBLE);
                        }
                    });
                }
            }
        }
        else if(beanResponseSell.getResponse().equals("denied")){
            progressDialog.dismiss();
            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?denied?"+beanResponseSell.getDescription() ;
            Common.setURL(newUrl);
            Intent intent = new Intent(pagosMit.this, Main.class);
            setResult(Activity.RESULT_OK, intent);
            myController.deviceDissconect();
            beanResponseSell.setCp("");
            finish();
        }
        else if(beanResponseSell.getResponse().equals("error")){
            progressDialog.dismiss();
            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?errorTransaccion?"+beanResponseSell.getDescription();
            Common.setURL(newUrl);
            Intent intent = new Intent(pagosMit.this, Main.class);
            setResult(Activity.RESULT_OK, intent);
            myController.deviceDissconect();
            myController = null;
            finish();
        }
        else{

            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?errorTransaccion" ;
            Common.setURL(newUrl);
            Intent intent = new Intent(pagosMit.this, Main.class);
            setResult(Activity.RESULT_OK, intent);
            myController.deviceDissconect();
            myController = null;
            finish();
        }
    }

    @Override
    public void didFinishTransactionWithMerchant(ArrayList<BeanMerchant> bean) {
        System.out.println("didFinishTransactionMerchant" + bean.toString());

    }

    @Override
    public void didFinishTransactionWithMerchantPDC(
            ArrayList<BeanMerchant> contado, ArrayList<BeanMerchant> msi,
            ArrayList<BeanMerchant> mci, String error) {
        System.out.println("didFinishTransactionWithMerchantPDC");

        progressDialog.setMessage(" valores contado"+ contado.toString());
    }

    @Override
    public void onRequestTextInfo(String msg) {
        if (! this.isFinishing()) {
            progressDialog.setTitle("Espere un momento");
            progressDialog.setMessage(msg);
            progressDialog.show();
        }
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

    @Override
    public void onDeviceUnplugged(String msg) {
        if(Device.equals("NomadWP2") || Device.equals("QPOS BT") || Device.equalsIgnoreCase("Walker BT")){
            if(msg.equalsIgnoreCase("Desconectado")){
                labelConnected.setTextColor(Color.parseColor("#e60000"));
                isDeviceSelected = false;
            }
            else{
                labelConnected.setTextColor(Color.parseColor("#8DC63F"));
                isDeviceSelected = true;
            }
            labelConnected.setText(msg);
            progressDialog.dismiss();
        }
        else{
            progressDialog.dismiss();
            //MessageBox("Dispositivo desconectado", msg, "Aceptar", pagosMit.this);
            progressDialog.setMessage(msg);
            progressDialog.show();
        }
    }

    @Override
    public void getTransaction(ArrayList<BeanResponseSell> list) {

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
    public void onReturnPnrInformation(MITPnrInformation pnrInformation, MitError error){

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
    public void onReturnLastTransaction(BeanResponseSell beanResponseSell,	MitError error) {

        //System.out.println("onReturnLastTransaction " + beanResponseSell + " error " + error);

    }

    private class ConnectDevice extends AsyncTask<Void, Void, Void> {

        @Override
        protected void onPreExecute() {

        }

        @Override
        protected Void doInBackground(Void... arg0) {
            myController.deviceConnect();
            return null;
        }
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
        Utilerias.println("Llega al delegado de multiaplicaci?n " + emvApplications.toString());
/*

        String[] appNameList = new String[emvApplications.size()];
        for (int i = 0; i < appNameList.length; ++i) {
            appNameList[i] = emvApplications.get(i);
        }

        final Dialog dialogList = new Dialog(this);
        dialogList.setCancelable(false);
        dialogList.setTitle("Seleccionar aplicaci?n");
        dialogList.setContentView(R.layout.dialog_applications);
        final ListView listView = (ListView)dialogList.findViewById(R.id.appList);
        listView.setAdapter(new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, appNameList));
        Button buttonCancel = (Button)dialogList.findViewById(R.id.cancelButton);

        listView.setOnItemClickListener(new OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                myController.setEmvApplication(position);
                dialogList.dismiss();
            }

        });

        buttonCancel.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                myController.setEmvApplication(null);
                dialogList.dismiss();
            }
        });
        dialogList.show();
*/
    }

}
//sndPay