package com.example.rodrigosanchez.zgm;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import java.io.File;


public class Opciones extends AppCompatActivity implements View.OnClickListener, AdapterView.OnItemSelectedListener{

    private Button btnSave;
    private EditText txtUrl;
    private EditText txtCorreo;
    private Spinner spn_Devices;
    DatabaseHandler dbh;
    String Device = "";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_opciones);
        dbh = new DatabaseHandler(getApplicationContext());
        txtUrl = (EditText) findViewById(R.id.txtUrl);
        txtCorreo = (EditText) findViewById(R.id.txtCorreo);
        btnSave = (Button) findViewById(R.id.btnGuardar);
        spn_Devices = (Spinner) findViewById(R.id.spn_device);
        spn_Devices.setOnItemSelectedListener(this);
        //btnSave.setOnClickListener(this);
        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String settings = dbh.getSettings();
                String correoS = dbh.getCorreo();
                String deviceS = dbh.getDevice();
                if(settings.contains("http")){
                    if(dbh.updateSettings(txtUrl.getText().toString()) == 1){
                        if(correoS.length() > 0){
                            if(dbh.updateCorreo(txtCorreo.getText().toString()) == 1){
                                if(deviceS.length() > 0){
                                    if(dbh.updateDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                                else{
                                    if(dbh.onInsertDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                            }
                            else{
                                Toast.makeText(getApplicationContext(),"Error al guardar el correo de confirmación",Toast.LENGTH_LONG).show();
                            }
                        }
                        else{
                            if(dbh.onInsertCorreo(txtCorreo.getText().toString()) == 1){
                                if(deviceS.length() > 0){
                                    if(dbh.updateDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                                else{
                                    if(dbh.onInsertDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                            }
                            else{
                                Toast.makeText(getApplicationContext(),"Error al guardar el correo de confirmación",Toast.LENGTH_LONG).show();
                            }
                        }
                    }
                    else{
                        Toast.makeText(getApplicationContext(),"Error al guardar la página de Inicio",Toast.LENGTH_LONG).show();
                    }
                }
                else{
                    if(dbh.onInsert(txtUrl.getText().toString()) == 1){
                        if(correoS.length() > 0){
                            if(dbh.updateCorreo(txtCorreo.getText().toString()) == 1){
                                if(deviceS.length() > 0){
                                    if(dbh.updateDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                                else{
                                    if(dbh.onInsertDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                            }
                            else{
                            }
                        }
                        else{
                            if(dbh.onInsertCorreo(txtCorreo.getText().toString()) == 1){
                                if(deviceS.length() > 0){
                                    if(dbh.updateDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                                else{
                                    if(dbh.onInsertDevice(Device) == 1){
                                        Common.setHomeURL(settings);
                                        Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                                        Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                                        startActivity(main);
                                        finish();
                                    }
                                    else{
                                        Toast.makeText(getApplicationContext(),"Error al guardar el dispositivo de pago",Toast.LENGTH_LONG).show();
                                    }
                                }
                            }
                            else{
                                Toast.makeText(getApplicationContext(),"Error al guardar el correo de confirmación",Toast.LENGTH_LONG).show();
                            }
                        }
                    }
                    else{
                        Toast.makeText(getApplicationContext(),"Error al guardar la página de Inicio",Toast.LENGTH_LONG).show();
                    }
                }
            }
        });
        getParametros();
    }

    public void getParametros() {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                txtUrl.setText(dbh.getSettings());
                txtCorreo.setText(dbh.getCorreo());
                Device = dbh.getDevice();
                if (Device.contains("WALKER BT"))
                    spn_Devices.setSelection(1);
                else if (Device.contains("QPOS BT"))
                    spn_Devices.setSelection(2);
                else
                    spn_Devices.setSelection(0);
            }
        });
    }

    public int saveSettings(String url) {
        return 0;//return dbh.updateSettings(url);
    }

    @Override
    public void onBackPressed() {
        Intent i = new Intent(getApplicationContext(),SplashLoad.class);
        startActivity(i);
        finish();
    }

    @Override
    public void onClick(View view) {
        String url = txtUrl.getText().toString();
        if(view.getId()== R.id.btnGuardar){
            int res = saveSettings(url);
            int bias = 0;
        }
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        Device = adapterView.getSelectedItem().toString();
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }
}
