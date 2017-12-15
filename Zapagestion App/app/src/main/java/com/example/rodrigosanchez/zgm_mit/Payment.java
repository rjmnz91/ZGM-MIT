package com.example.rodrigosanchez.zgm_mit;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.StrictMode;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
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
import java.util.ArrayList;
import java.util.Random;
import java.util.concurrent.TimeoutException;

public class Payment extends AppCompatActivity implements View.OnClickListener, MitControllerListener, AdapterView.OnItemSelectedListener{

    private EditText txtMonto, txtReferencia, txtNumero;
    private Spinner spn_plazo, spn_company;
    private Button btn_cancel, btnConect, btn_start;
    private TextView txtSMSH, txtCompany, message;
    private MitController myController;
    private BeanData data;
    private ProgressDialog progressDialog;
    private String image,amount,merchant,usrTrx, refPay, email,tarjeta,tipoMit,auth,operation,accion,company,branch,user,password,operationType,country,currency;
    private boolean isDeviceSelected;
    String Device = "", correoComercio = "", opcionSms = "", cad = "", plazo = "", carrier = "";
    DatabaseHandler dbh;
    ArrayList<Merchant> merchants = new ArrayList<>();
    ArrayList<String> merchantName = new ArrayList<>();
    ArrayList<String> merchantNumber = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_payment);

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        init();
        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));
    }

    private void init(){
        dbh = new DatabaseHandler(getApplicationContext());
        data = new BeanData();
        progressDialog = new ProgressDialog(this);
        //botones
        btn_cancel = (Button)findViewById(R.id.btn_cancel);
        btnConect = (Button)findViewById(R.id.btnConect);
        btn_start = (Button)findViewById(R.id.btn_start);
        btn_cancel.setOnClickListener(this);
        btnConect.setOnClickListener(this);
        btn_start.setOnClickListener(this);
        //etiquetas y textos
        message = (TextView)findViewById(R.id.message);
        txtMonto = (EditText)findViewById(R.id.txtMonto);
        txtReferencia = (EditText)findViewById(R.id.txtReferencia);
        spn_plazo = (Spinner)findViewById(R.id.spn_plazo);
        txtSMSH = (TextView)findViewById(R.id.txtSMSH);
        txtNumero = (EditText)findViewById(R.id.txtNumero);
        txtCompany = (TextView)findViewById(R.id.txtCompany);
        spn_company = (Spinner)findViewById(R.id.spn_company);
        spn_plazo.setOnItemSelectedListener(this);
        spn_company.setOnItemSelectedListener(this);
        //variables
        Common.setCompany(dbh.getSettings("company"));
        //company = data.getCompany();
        Common.setBranch(dbh.getSettings("branch"));
        //branch = data.getBranch();
        Common.setUser(dbh.getSettings("usuario"));
        //user = data.getUser();
        Common.setPassword(dbh.getSettings("password"));
        //password = data.getPwd();
        //operationType = data.getOperationType();
        //Common.setCountry(dbh.getSettings("country"));
        //country = data.getCountry();
        //Common.setCurrency(dbh.getSettings("currency"));
        //currency = data.getCurrency();
        correoComercio = dbh.getSettings("correo");
        Device = dbh.getSettings("device");
        opcionSms = dbh.getSettings("sms");
        accion = "0";
        isDeviceSelected = false;
        //call from Main
        myController = new MitController(Payment.this);
        //myController.setURL(data.getServer());
        String service = Common.getService();
        myController.setURL(service);
        Intent intent = getIntent();
        if(intent != null){
            //String[] values = intent.getStringArrayExtra("datos");
            //refPay = values[0];
            //amount = values[1];
            //email = values[2];
            refPay = intent.getStringExtra("vtaRef");
            amount = intent.getStringExtra("ttlPago");
            email = intent.getStringExtra("correo");
            //accion = intent.getStringExtra("accion");
        }
        //UI
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                String qty = amount.replace("%20","");
                qty = qty.replace(",","");
                txtMonto.setText(qty);
                txtReferencia.setText(refPay);
                if(opcionSms.equals("1")){
                    txtSMSH.setVisibility(View.VISIBLE);
                    txtNumero.setVisibility(View.VISIBLE);
                    txtCompany.setVisibility(View.VISIBLE);
                    spn_company.setVisibility(View.VISIBLE);
                }
            }
        });


    }

    @Override
    public void onClick(View view) {
        if(view.getId() == btn_start.getId()){
            if(opcionSms.equals("1")){
                if(txtNumero.length()>0){
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            spn_plazo.setEnabled(false);
                            txtNumero.setEnabled(false);
                            spn_company.setEnabled(false);
                            btn_cancel.setEnabled(false);
                            btn_start.setEnabled(false);
                        }
                    });
                }else{
                    Toast.makeText(getApplicationContext(),"Por favor introduzca el número de telefono donde desea recibir su comprobante.",Toast.LENGTH_LONG).show();
                }
            }else{
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        spn_plazo.setEnabled(false);
                        txtNumero.setEnabled(false);
                        spn_company.setEnabled(false);
                        btn_cancel.setEnabled(false);
                        btn_start.setEnabled(false);
                    }
                });
            }
            if(Device.equals("VX600")){
                setPaymentMode();
            }else{
                if(Device.equals("NomadWP2") || Device.equals("QPOS BT") || Device.equalsIgnoreCase("Walker BT")){
                    if(isDeviceSelected)
                        sndProcess();
                    else
                        new ConnectDevice().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
                }
                else{
                    sndProcess();
                }
            }
        }else if(view.getId() == btn_cancel.getId()){
            finish();
            //android.os.Process.killProcess(android.os.Process.myPid());
        }else if(view.getId() == btnConect.getId()){
            if(Device.equalsIgnoreCase("QPOS BT")){
                myController.setDevice(PaymentDevice.QPOS_BT);
            }else if(Device.equalsIgnoreCase("Walker BT")){
                myController.setDevice(PaymentDevice.WALKER_BT);
            }
            new ConnectDevice().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        }
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        if(adapterView.getId() == spn_plazo.getId()){
            //merchant = getResources().getStringArray(R.array.l)[spn_plazo.getSelectedItemPosition()];
            //plazo = adapterView.getSelectedItem().toString();
            merchant = merchantNumber.get(spn_plazo.getSelectedItemPosition()).toString();
            if(merchant != "")
                myController.sndPay(merchant);
            else
                ;
        }if(adapterView.getId() == spn_company.getId()){
            carrier = getResources().getStringArray(R.array.PhoneCompanyValue)[spn_company.getSelectedItemPosition()];
        }
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }

    @Override
    public void isFinishedCardReader(MITCardInformation mitCardInformation) {
        myController.sndPay("MIT_CONSULTA");
        //myController.sndPay(merchant);
    }

    @Override
    public void setMitError(final MitError mitError) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                progressDialog.dismiss();
                String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?"+mitError.getDescripcion() ;
                Common.setURL(newUrl);
                finish();
                startActivity(new Intent(Payment.this,Zapagestion.class));

            }
        });
    }
@Override
    public void setResult(String s) {
        progressDialog.dismiss();
        if(s.equalsIgnoreCase("Desconectado")){
            message.setTextColor(Color.parseColor("#e60000"));
        }else{
            message.setTextColor(Color.parseColor("#8DC63F"));
        }
        message.setText(s);
        enableButton();
    }

    public void enableButton(){
        if(message.getText().toString().equalsIgnoreCase("Conectado")){
            runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    btn_start.setVisibility(View.VISIBLE);
                    //btn_start.setBackgroundColor(Color.parseColor("#63c573"));
                    btnConect.setVisibility(View.INVISIBLE);
                }
            });
        }
    }

    @Override
    public void setResult(BeanResponseSell beanResponseSell, String s) {
        progressDialog.dismiss();
        if (beanResponseSell.getResponse().equals("approved")){
            progressDialog.dismiss();
            auth = beanResponseSell.getAuth();
            operation = beanResponseSell.getFoliocpagos();
            tarjeta = beanResponseSell.getCc_number();
            tipoMit = beanResponseSell.getAppidlabel();

            String response = "Response " +  beanResponseSell.getResponse() + "\n";
            response += "Folio " +  beanResponseSell.getFoliocpagos() + "\n";
            response += "Date " +  beanResponseSell.getDate() + "\n";
            response += "Time " +  beanResponseSell.getTime() + "\n";
            response += "Auth " +  beanResponseSell.getAuth() + "\n";

            if(beanResponseSell.getCp() != null && beanResponseSell.getCp().equals("1")){
                try{
                    myController.sndEmailWithAddress(email,correoComercio,operation,Common.getUser(),Common.getPassword(),Common.getCompany(),Common.getBranch(),Common.getCountry());
                    String newUrl = Common.getHomeURL()+"/CarritoDetalle.aspx?"+amount+"$"+tarjeta+"$"+tipoMit+"$"+merchant+"$"+email+ "$" + auth + "$" + operation;
                    Common.setURL(newUrl);
                    if(opcionSms.equals("1")){
                        myController.sndSMS(txtNumero.getText().toString(),carrier,operation,Common.getUser(),Common.getPassword(),Common.getCompany(),Common.getBranch());
                    }else{
                    }
                }catch(TimeoutException te){
                    te.printStackTrace();
                }
                finish();
                //android.os.Process.killProcess(android.os.Process.myPid());
                Intent i = new Intent(Payment.this, Zapagestion.class);
                startActivity(i);

            }else{
                if(beanResponseSell.getStQPS().equals("1")){
                    try{
                        myController.sndEmailWithAddress(email,correoComercio,operation,Common.getUser(),Common.getPassword(),Common.getCompany(),Common.getBranch(),Common.getCountry());
                        String newUrl = Common.getHomeURL()+"/CarritoDetalle.aspx?"+amount+"$"+tarjeta+"$"+tipoMit+"$"+merchant+"$"+email+ "$" + auth + "$" + operation;
                        Common.setURL(newUrl);
                        if(opcionSms.equals("1")){
                            myController.sndSMS(txtNumero.getText().toString(),carrier,operation,Common.getUser(),Common.getPassword(),Common.getCompany(),Common.getBranch());
                        }else{
                        }
                    }catch(TimeoutException te){
                        te.printStackTrace();
                    }
                    finish();
                    //android.os.Process.killProcess(android.os.Process.myPid());
                    Intent i = new Intent(Payment.this, Zapagestion.class);
                    startActivity(i);
                }else{
                    finish();
                    Intent i = new Intent(Payment.this, Signature.class);
                    i.putExtra("cad_sign",s);
                    i.putExtra("folio",operation);
                    i.putExtra("email",email);
                    i.putExtra("correoComercio",correoComercio);
                    i.putExtra("operation",operation);
                    i.putExtra("user",Common.getUser());
                    i.putExtra("password",Common.getPassword());
                    i.putExtra("company",Common.getCompany());
                    i.putExtra("branch",Common.getBranch());
                    i.putExtra("country",Common.getCountry());
                    i.putExtra("amount",amount);
                    i.putExtra("tarjeta",tarjeta);
                    i.putExtra("tipoMit",tipoMit);
                    i.putExtra("merchant",merchant);
                    i.putExtra("auth",auth);
                    i.putExtra("sms",opcionSms);
                    if(opcionSms.equals("1")) {
                        i.putExtra("numero", txtNumero.getText().toString());
                        i.putExtra("carrier", carrier);
                    }else {
                    }
                    startActivity(i);
                }
            }
        }else if(beanResponseSell.getResponse().equals("denied")){
            progressDialog.dismiss();
            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?denied?"+beanResponseSell.getDescription() ;
            Common.setURL(newUrl);
            Intent i = new Intent(Payment.this, Zapagestion.class);
            finish();
            startActivity(i);
        }else if(beanResponseSell.getResponse().equals("error")){
            progressDialog.dismiss();
            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?errorTransaccion?"+beanResponseSell.getDescription();
            Common.setURL(newUrl);
            Intent i = new Intent(Payment.this, Zapagestion.class);
            finish();
            startActivity(i);
        }else{
            progressDialog.dismiss();
            String newUrl = Common.getHomeURL() + "/Error.aspx?Inicio.aspx?errorTransaccion" ;
            Common.setURL(newUrl);
            Intent i = new Intent(Payment.this, Zapagestion.class);
            finish();
            startActivity(i);
        }
    }

    @Override
    public void didFinishTransactionWithMerchant(ArrayList<BeanMerchant> arrayList) {
        System.out.println("didFinishTransactionMerchant" + arrayList.toString());
    }

    @Override
    public void didFinishTransactionWithMerchantPDC(ArrayList<BeanMerchant> arrayList, ArrayList<BeanMerchant> arrayList1, ArrayList<BeanMerchant> arrayList2, String s) {
        //System.out.println("didFinishTransactionWithMerchantPDC");
        merchants.add(new Merchant("",""));
        merchantName.add("");
        merchantNumber.add("");
        addMerchant(arrayList,merchants);
        if (arrayList1.size() > 0)
            addMerchant(arrayList1, merchants);
        if (arrayList2.size() > 0)
            addMerchant(arrayList2, merchants);
        ArrayAdapter<Merchant> merchantArrayAdapter = new ArrayAdapter<Merchant>(getApplicationContext(),R.layout.support_simple_spinner_dropdown_item,merchants);
        merchantArrayAdapter.setDropDownViewResource(R.layout.support_simple_spinner_dropdown_item);
        spn_plazo.setAdapter(merchantArrayAdapter);
        spn_plazo.setEnabled(true);
        progressDialog.dismiss();
        //progressDialog.setMessage(" valores contado"+ arrayList.toString());
    }

    public void addMerchant(ArrayList<BeanMerchant> arrayList, ArrayList<Merchant> merchants){
        for(int x=0; x < arrayList.size(); x++) {
            BeanMerchant data = arrayList.get(x);
            merchants.add(new Merchant(data.getNameMerchant(),data.getNumberMerchant()));
            merchantName.add(data.getNameMerchant());
            merchantNumber.add(data.getNumberMerchant());
        }
    }

    @Override
    public void onRequestTextInfo(String s) {
        if (! this.isFinishing()) {
            progressDialog.setTitle("Espere un momento");
            progressDialog.setMessage(s);
            progressDialog.show();
        }
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
        if(Device.equals("NomadWP2") || Device.equals("QPOS BT") || Device.equals("Walker BT")){
            if(s.equalsIgnoreCase("Desconectado")){
                message.setTextColor(Color.parseColor("#e60000"));
                isDeviceSelected = false;
            }
            else{
                message.setTextColor(Color.parseColor("#8DC63F"));
                isDeviceSelected = true;
            }
            message.setText(s);
            progressDialog.dismiss();
        }
        else{
            progressDialog.dismiss();
            //MessageBox("Dispositivo desconectado", msg, "Aceptar", Sell.this);
            progressDialog.setMessage(s);
            progressDialog.show();
        }
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

    //
    private class ConnectDevice extends AsyncTask<Void, Void, Void> {

        @Override
        protected void onPreExecute() {

        }

        @Override
        protected Void doInBackground(Void... arg0) {
            myController.deviceConnect();
            enableButton();
            return null;
        }
    }

    private void setPaymentMode(){
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Payment.this);
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
        amount = txtMonto.getText().toString();
        usrTrx = Common.getUser();
        refPay = txtReferencia.getText().toString().trim(); //+ randomNum;

		/*amount = txt_amount.getText().toString();
		usrTrx = user;
		refPay = txt_reference.getText().toString().trim();*/
        //myController.setReset(true);
        company = Common.getCompany();
        branch = Common.getBranch();
        user = Common.getUser();
        password = Common.getPassword();
        operationType = Common.getOperationType();
        country = Common.getCountry();
        currency = Common.getCurrency();
        myController.sndEmvDirectSellWithAmount(amount, company, branch,user, password, usrTrx, merchant, refPay, operationType,country, currency, "");
    }

}