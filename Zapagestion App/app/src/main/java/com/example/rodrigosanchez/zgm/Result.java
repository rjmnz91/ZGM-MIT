package com.example.rodrigosanchez.zgm;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.mitec.beans.BeanResponseSell;
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
import android.widget.LinearLayout;
import android.widget.TextView;

public class Result extends AppCompatActivity implements OnClickListener{
    private Button btn_ok;
    private TextView txt_info;
    private String cad_info;
    private String cad_sign;
    private String error;
    private String cad_folio;
    private String message;
    private String email;
    private boolean exit;
    private boolean isSearch;

    private TextView response;
    private TextView reference;
    private TextView folio;
    private TextView auth;
    private TextView time;
    private TextView date;
    private TextView company;
    private TextView merchant;
    private TextView street;
    private TextView ccType;
    private TextView tpOperation;
    private TextView ccName;
    private TextView ccNumber;
    private TextView month;
    private TextView year;
    private TextView voucherCliente;
    private TextView voucherComercio;
    private TextView amount;
    private TextView appIdLabel;
    private TextView bank;
    private TextView mark;
    private TextView arqc;
    private TextView appid;
    private TextView errorCode;
    private TextView description;

    private LinearLayout layoutResponse;
    private LinearLayout layoutReference;
    private LinearLayout layoutFolio;
    private LinearLayout layoutAuth;
    private LinearLayout layoutTime;
    private LinearLayout layoutDate;
    private LinearLayout layoutCompany;
    private LinearLayout layoutMerchant;
    private LinearLayout layoutStreet;
    private LinearLayout layoutCctype;
    private LinearLayout layoutTpOperation;
    private LinearLayout layoutName;
    private LinearLayout layoutCcNumber;
    private LinearLayout layoutMonth;
    private LinearLayout layoutYear;
    private LinearLayout layoutVocuherCliente;
    private LinearLayout layoutVoucherComercio;
    private LinearLayout layoutAmount;
    private LinearLayout layoutAppIdLabel;
    private LinearLayout layoutBank;
    private LinearLayout layoutMark;
    private LinearLayout layoutArqc;
    private LinearLayout layoutAppid;
    private LinearLayout layoutError;
    private LinearLayout layoutDescrtiption;
    BeanResponseSell beanResponseSell;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_result);

        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        init();

        ActionBar bar = getSupportActionBar();
        bar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#0266a9")));
    }

    private void init(){
        txt_info = (TextView) findViewById(R.id.txt_info);
        btn_ok = (Button) findViewById(R.id.btn_ok);
        btn_ok.setOnClickListener(this);

        response = (TextView)findViewById(R.id.response);
        reference = (TextView)findViewById(R.id.reference);
        folio  = (TextView)findViewById(R.id.folio);
        auth = (TextView)findViewById(R.id.auth);
        time  = (TextView)findViewById(R.id.time);
        date  = (TextView)findViewById(R.id.date);
        company = (TextView)findViewById(R.id.company);
        merchant = (TextView)findViewById(R.id.merchant);
        street = (TextView)findViewById(R.id.street);
        ccType = (TextView)findViewById(R.id.type);
        tpOperation = (TextView)findViewById(R.id.operation);
        ccName = (TextView)findViewById(R.id.name);
        ccNumber = (TextView)findViewById(R.id.number);
        month = (TextView)findViewById(R.id.expMonth);
        year = (TextView)findViewById(R.id.expYear);
        voucherCliente = (TextView)findViewById(R.id.vocuherCliente);
        voucherComercio = (TextView)findViewById(R.id.voucherComercio);
        amount = (TextView)findViewById(R.id.amount);
        appIdLabel = (TextView)findViewById(R.id.appIdLabel);
        bank = (TextView)findViewById(R.id.ccBank);
        mark = (TextView)findViewById(R.id.ccMark);
        arqc = (TextView)findViewById(R.id.arqc);
        appid = (TextView)findViewById(R.id.appId);
        errorCode = (TextView)findViewById(R.id.codeError);
        description = (TextView)findViewById(R.id.description);

        layoutResponse = (LinearLayout)findViewById(R.id.layoutResponse);
        layoutReference = (LinearLayout)findViewById(R.id.layoutReference);
        layoutFolio = (LinearLayout)findViewById(R.id.layoutFolio);
        layoutAuth = (LinearLayout)findViewById(R.id.layoutAuth);
        layoutTime = (LinearLayout)findViewById(R.id.layoutTime);
        layoutDate = (LinearLayout)findViewById(R.id.layoutDate);
        layoutCompany = (LinearLayout)findViewById(R.id.layoutCompany);
        layoutMerchant = (LinearLayout)findViewById(R.id.layoutMerchant);
        layoutStreet = (LinearLayout)findViewById(R.id.layoutStreet);
        layoutCctype = (LinearLayout)findViewById(R.id.layoutCctype);
        layoutTpOperation = (LinearLayout)findViewById(R.id.layoutTpOperation);
        layoutName = (LinearLayout)findViewById(R.id.layoutName);
        layoutCcNumber = (LinearLayout)findViewById(R.id.layoutNumber);
        layoutMonth = (LinearLayout)findViewById(R.id.layoutExpMonth);
        layoutYear = (LinearLayout)findViewById(R.id.layoutExpYear);
        layoutVocuherCliente = (LinearLayout)findViewById(R.id.layoutVoucherCliente);
        layoutVoucherComercio = (LinearLayout)findViewById(R.id.layoutVocuherComercio);
        layoutAmount = (LinearLayout)findViewById(R.id.layoutAmount);
        layoutAppIdLabel = (LinearLayout)findViewById(R.id.layoutAppIdLabel);
        layoutBank = (LinearLayout)findViewById(R.id.layoutCcbank);
        layoutMark = (LinearLayout)findViewById(R.id.layoutCcmark);
        layoutArqc = (LinearLayout)findViewById(R.id.layoutArqc);
        layoutAppid = (LinearLayout)findViewById(R.id.layoutAppid);
        layoutError = (LinearLayout)findViewById(R.id.layoutError);
        layoutDescrtiption = (LinearLayout)findViewById(R.id.layoutDescription);


        Intent intent = getIntent();

        if (intent != null) {
            cad_sign = intent.getStringExtra("cad_sign");
            cad_info = intent.getStringExtra("response");
            error = intent.getStringExtra("error");
            cad_folio = intent.getStringExtra("folio");
            message = intent.getStringExtra("message");
            beanResponseSell = (BeanResponseSell)intent.getSerializableExtra("beanResponseSell");
            isSearch = intent.getBooleanExtra("isSearch", false);
            email = intent.getStringExtra("correo");

            if(beanResponseSell != null){
                //System.out.println("beanResponseSell " + beanResponseSell);
                if(beanResponseSell.getResponse() != null && !beanResponseSell.getResponse().equals("")){
                    layoutResponse.setVisibility(View.VISIBLE);
                    response.setText(beanResponseSell.getResponse());
                }
                if(beanResponseSell.getReference() != null && !beanResponseSell.getReference().equals("")){
                    layoutReference.setVisibility(View.VISIBLE);
                    reference.setText(beanResponseSell.getReference());
                }
                if(beanResponseSell.getFoliocpagos() != null && !beanResponseSell.getFoliocpagos().equals("")){
                    layoutFolio.setVisibility(View.VISIBLE);
                    folio.setText(beanResponseSell.getFoliocpagos());
                }
                if(beanResponseSell.getAuth() != null && !beanResponseSell.getAuth().equals("")){
                    layoutAuth.setVisibility(View.VISIBLE);
                    auth.setText(beanResponseSell.getAuth());
                }
                if(beanResponseSell.getTime() != null && !beanResponseSell.getTime().equals("")){
                    layoutTime.setVisibility(View.VISIBLE);
                    time.setText(beanResponseSell.getTime());
                }
                if(beanResponseSell.getDate() != null && !beanResponseSell.getDate().equals("")){
                    layoutDate.setVisibility(View.VISIBLE);
                    date.setText(beanResponseSell.getDate());
                }
                if(beanResponseSell.getNb_company() != null && !beanResponseSell.getNb_company().equals("")){
                    layoutCompany.setVisibility(View.VISIBLE);
                    company.setText(beanResponseSell.getNb_company());
                }
                if(beanResponseSell.getNb_merchant() != null && !beanResponseSell.getNb_merchant().equals("")){
                    layoutMerchant.setVisibility(View.VISIBLE);
                    merchant.setText(beanResponseSell.getNb_merchant());
                }
                if(beanResponseSell.getNb_street() != null && !beanResponseSell.getNb_street().equals("")){
                    layoutStreet.setVisibility(View.VISIBLE);
                    street.setText(beanResponseSell.getNb_street());
                }
                if(beanResponseSell.getCc_type() != null && !beanResponseSell.getCc_type().equals("")){
                    layoutCctype.setVisibility(View.VISIBLE);
                    ccType.setText(beanResponseSell.getCc_type());
                }
                if(beanResponseSell.getTp_operation() != null && !beanResponseSell.getTp_operation().equals("")){
                    layoutTpOperation.setVisibility(View.VISIBLE);
                    tpOperation.setText(beanResponseSell.getTp_operation());
                }
                if(beanResponseSell.getCc_name() != null && !beanResponseSell.getCc_name().equals("")){
                    layoutName.setVisibility(View.VISIBLE);
                    ccName.setText(beanResponseSell.getCc_name());
                }
                if(beanResponseSell.getCc_number() != null && !beanResponseSell.getCc_number().equals("")){
                    layoutCcNumber.setVisibility(View.VISIBLE);
                    ccNumber.setText(beanResponseSell.getCc_number());
                }
                if(beanResponseSell.getCc_expmonth() != null && !beanResponseSell.getCc_expmonth().equals("")){
                    layoutMonth.setVisibility(View.VISIBLE);
                    month.setText(beanResponseSell.getCc_expmonth());
                }
                if(beanResponseSell.getCc_expyear() != null && !beanResponseSell.getCc_expyear().equals("")){
                    layoutYear.setVisibility(View.VISIBLE);
                    year.setText(beanResponseSell.getCc_expyear());
                }
                if(beanResponseSell.getVoucher_cliente() != null && !beanResponseSell.getVoucher_cliente().equals("")){
                    layoutVocuherCliente.setVisibility(View.VISIBLE);
                    voucherCliente.setText(beanResponseSell.getVoucher_cliente());
                }
                if(beanResponseSell.getVoucher_comercio() != null && !beanResponseSell.getVoucher_comercio().equals("")){
                    layoutVoucherComercio.setVisibility(View.VISIBLE);
                    voucherComercio.setText(beanResponseSell.getVoucher_comercio());
                }
                if(beanResponseSell.getAmount() != null && !beanResponseSell.getAmount().equals("")){
                    layoutAmount.setVisibility(View.VISIBLE);
                    amount.setText(beanResponseSell.getAmount());
                }
                if(beanResponseSell.getAppidlabel() != null && !beanResponseSell.getAppidlabel().equals("")){
                    layoutAppIdLabel.setVisibility(View.VISIBLE);
                    appIdLabel.setText(beanResponseSell.getAppidlabel());
                }
                if(beanResponseSell.getCcBank() != null && !beanResponseSell.getCcBank().equals("")){
                    layoutBank.setVisibility(View.VISIBLE);
                    bank.setText(beanResponseSell.getCcBank());
                }
                if(beanResponseSell.getCcMark() != null && !beanResponseSell.getCcMark().equals("")){
                    layoutMark.setVisibility(View.VISIBLE);
                    mark.setText(beanResponseSell.getCcMark());
                }
                if(beanResponseSell.getArqc() != null && !beanResponseSell.getArqc().equals("")){
                    layoutArqc.setVisibility(View.VISIBLE);
                    arqc.setText(beanResponseSell.getArqc());
                }
                if(beanResponseSell.getAppid() != null && !beanResponseSell.getAppid().equals("")){
                    layoutAppid.setVisibility(View.VISIBLE);
                    appid.setText(beanResponseSell.getAppid());
                }
                if(beanResponseSell.getCd_error() != null && !beanResponseSell.getCd_error().equals("")){
                    layoutError.setVisibility(View.VISIBLE);
                    errorCode.setText(beanResponseSell.getCd_error());
                }
                if(beanResponseSell.getDescription() != null && !beanResponseSell.getDescription().equals("")){
                    layoutDescrtiption.setVisibility(View.VISIBLE);
                    description.setText(beanResponseSell.getDescription());
                }
            }
        }

        if(error!=null && !error.equalsIgnoreCase("")){
            txt_info.setText(error);
            //linearBeanResponseSell.setVisibility(View.GONE);
            exit = true;
        }


        if(cad_info!=null && !cad_info.equalsIgnoreCase("")){
            txt_info.setText(cad_info);
        }

        if(message!=null && !message.equalsIgnoreCase("")){
            txt_info.setText(message);
            exit = true;
        }
        if(cad_sign!=null && !cad_sign.equalsIgnoreCase("")){
            txt_info.setText("Venta exitosa");
        }
        if(isSearch){
            exit = true;
            txt_info.setText("Resultado de la búsqueda");
        }
    }

    @Override
    public void onBackPressed() {

    }
    @Override
    public void onClick(View view) {

        switch (view.getId()) {
            case R.id.btn_ok:
                if(!exit){
                    finish();
                    Intent i = null;
                    if(beanResponseSell.getCp() != null && beanResponseSell.getCp().equals("1"))
                       i = new Intent(Result.this, SetEmail.class);
                    else
                      i = new Intent(Result.this, Signature.class);
                    i.putExtra("cad_sign", cad_sign);
                    i.putExtra("folio",cad_folio);
                    i.putExtra("correo",email);
                    i.putExtra("amount",beanResponseSell.getAmount());
                    i.putExtra("tarjeta",beanResponseSell.getCc_number());
                    i.putExtra("tipoMit",beanResponseSell.getAppidlabel());
                    i.putExtra("merchant",beanResponseSell.getNb_merchant());
                    startActivity(i);
                }
                else{
                    finish();
                    Intent i = new Intent(Result.this, Main.class);
                    //android.os.Process.killProcess(android.os.Process.myPid());
                    //startActivity(i);
                }
                break;

            default:
                break;
        }
    }


}