package com.example.rodrigosanchez.zgm_mit;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.InputType;
import android.text.method.PasswordTransformationMethod;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class Opciones extends AppCompatActivity implements AdapterView.OnItemSelectedListener {

    private TextView mTextMessage;
    private EditText txtUrl, txtCorreo;
    private Spinner spn_Devices;
    private CheckBox cbSMS;
    DatabaseHandler dbh;
    String Device = "", SMS = "0";
    private Button btnSave, btnAvanzada;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_opciones);

        mTextMessage = (TextView) findViewById(R.id.message);
        dbh = new DatabaseHandler(getApplicationContext());
        txtUrl = (EditText) findViewById(R.id.txtUrl);
        txtCorreo = (EditText) findViewById(R.id.txtCorreo);
        btnSave = (Button) findViewById(R.id.btnGuardar);
        btnAvanzada = (Button) findViewById(R.id.btnAvanzada);
        spn_Devices = (Spinner) findViewById(R.id.spn_device);
        spn_Devices.setOnItemSelectedListener(this);
        cbSMS = (CheckBox) findViewById(R.id.cbSMS);
        cbSMS.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                if(cbSMS.isChecked()) {
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            cbSMS.setText("Activado");
                            SMS = "1";
                        }
                    });
                }else{
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            cbSMS.setText("Desctivado");
                            SMS = "0";
                        }
                    });
                }
            }
        });

        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String settings = dbh.getSettings("url"), correoS  = dbh.getSettings("correo"), deviceS  = dbh.getSettings("device"), mensajeS = dbh.getSettings("sms");
                String urlValue = txtUrl.getText().toString(), correoValue = txtCorreo.getText().toString();


                if(settings.equalsIgnoreCase(urlValue)){

                }else {
                    if (settings.contains("http")) {
                        if (dbh.updateSettings("url", urlValue) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar la página de Inicio", Toast.LENGTH_LONG).show();
                            return;
                        }
                    } else {
                        if (dbh.onInsert("url", urlValue) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar la página de Inicio", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(correoS.equalsIgnoreCase(correoValue)){

                }else {
                    if (correoS.length() > 0) {
                        if (dbh.updateSettings("correo", txtCorreo.getText().toString()) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar el correo de confirmación", Toast.LENGTH_LONG).show();
                            return;
                        }
                    } else {
                        if (dbh.onInsert("correo", txtCorreo.getText().toString()) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar el correo de confirmación", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(deviceS.equalsIgnoreCase(Device)){

                }else {
                    if (deviceS.length() > 0) {
                        if (dbh.updateSettings("device", Device) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar el dispositivo de pago", Toast.LENGTH_LONG).show();
                            return;
                        }
                    } else {
                        if (dbh.onInsert("device", Device) == 1) {

                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar el dispositivo de pago", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(mensajeS.equalsIgnoreCase(SMS)){
                    Common.setHomeURL(settings);
                    Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                    Intent main = new Intent(getApplicationContext(), Main.class);
                    startActivity(main);
                    finish();
                }else {
                    if (mensajeS.length() > 0) {
                        if (dbh.updateSettings("sms", SMS) == 1) {
                            Common.setHomeURL(settings);
                            Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                            Intent main = new Intent(getApplicationContext(), Main.class);
                            startActivity(main);
                            finish();
                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar la configuración de SMS", Toast.LENGTH_LONG).show();
                            return;
                        }
                    } else {
                        if (dbh.onInsert("sms", SMS) == 1) {
                            Common.setHomeURL(settings);
                            Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                            Intent main = new Intent(getApplicationContext(), Main.class);
                            startActivity(main);
                            finish();
                        } else {
                            Toast.makeText(getApplicationContext(), "Error al guardar la configuración de SMS", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }
            }
        });

        getParametros();

        btnAvanzada.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                AlertDialog.Builder builder = new AlertDialog.Builder(Opciones.this);
                builder.setTitle("Configuración");

                final EditText input = new EditText(Opciones.this);
                input.setInputType(InputType.TYPE_CLASS_NUMBER);
                input.setTransformationMethod(PasswordTransformationMethod.getInstance());
                builder.setView(input);
                builder.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        if(openAdvanceSetting(Integer.parseInt(input.getText().toString()))){
                            Intent advanced = new Intent(getApplicationContext(), avanzada.class);
                            startActivity(advanced);
                            finish();
                        }
                        else{
                            Toast.makeText(getApplicationContext(), "El código introducido es incorrecto", Toast.LENGTH_LONG).show();
                            dialogInterface.cancel();
                        }
                    }
                });
                builder.setNegativeButton("Cancelar", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.cancel();
                    }
                });

                builder.show();
            }
        });
    }

    public boolean openAdvanceSetting(int code){
        Date current = Calendar.getInstance().getTime();
        int m = getMonthValue(current), d = getDayValue(current);
        double pass = m*d;
        pass = Math.pow(pass,3);
        if(code == pass)
            return true;
        else
            return false;
    }

    public int getMonthValue(Date date){
        String format = "M";
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat(format);
        int month = Integer.parseInt(simpleDateFormat.format(date));
        return  month;
    }

    public int getDayValue(Date date){
        String format = "dd";
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat(format);
        int day = Integer.parseInt(simpleDateFormat.format(date));
        return  day;
    }

    public void getParametros() {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                txtUrl.setText(dbh.getSettings("url"));
                txtCorreo.setText(dbh.getSettings("correo"));
                Device = dbh.getSettings("device");
                SMS = dbh.getSettings("sms");
                if (Device.contains("WALKER BT"))
                    spn_Devices.setSelection(1);
                else if (Device.contains("QPOS BT"))
                    spn_Devices.setSelection(2);
                else
                    spn_Devices.setSelection(0);
                if(SMS.equals("1")){
                    cbSMS.setChecked(true);
                    cbSMS.setText("Activado");
                }else {
                    cbSMS.setChecked(false);
                    cbSMS.setText("Desctivado");
                }
            }
        });
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        Device = adapterView.getSelectedItem().toString();
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }
}
