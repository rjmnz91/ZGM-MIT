package com.example.rodrigosanchez.zgm_mit;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class avanzada extends AppCompatActivity {

    private EditText txtService, txtCompany, txtBranch, txtUsuario, txtPassword;
    DatabaseHandler dbh;
    private Button btnCancelar, btnGuardar;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_avanzada);

        dbh = new DatabaseHandler(getApplicationContext());
        txtService = (EditText) findViewById(R.id.txtService);
        txtUsuario = (EditText) findViewById(R.id.txtUsuario);
        txtPassword = (EditText) findViewById(R.id.txtPassword);
        txtCompany = (EditText) findViewById(R.id.txtCompany);
        txtBranch = (EditText) findViewById(R.id.txtBranch);
        btnCancelar = (Button) findViewById(R.id.btnCancelar);
        btnGuardar = (Button) findViewById(R.id.btnGuardar);

        getParametros();

        btnCancelar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent opciones = new Intent(getApplicationContext(), Opciones.class);
                startActivity(opciones);
                finish();
            }
        });

        btnGuardar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String serviceC = dbh.getSettings("service"), companyC = dbh.getSettings("company"),
                        branchC = dbh.getSettings("branch"), usuarioC = dbh.getSettings("usuario"),
                        passC = dbh.getSettings("password");
                String serviceValue = txtService.getText().toString(), companyValue = txtCompany.getText().toString(),
                        branchValue = txtBranch.getText().toString(), usuarioValue = txtUsuario.getText().toString(),
                        passValue = txtPassword.getText().toString();

                if(serviceC.equalsIgnoreCase(serviceValue)){

                }else{
                    if(serviceC.length()>0){
                        if(dbh.updateSettings("service",serviceValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el servicio", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }else{
                        if(dbh.onInsert("service",serviceValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el servicio", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(companyC.equalsIgnoreCase(companyValue)){

                }else{
                    if(companyC.length() > 0){
                        if(dbh.updateSettings("company",companyValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el id de Compañia", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }else{
                        if(dbh.onInsert("company",companyValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el id de Compañia", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(branchC.equalsIgnoreCase(branchValue)){

                }else{
                    if(branchC.length()>0){
                        if(dbh.updateSettings("branch",branchValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el id de Sucursal", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }else{
                        if(dbh.onInsert("branch",branchValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el id de Sucursal", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(usuarioC.equalsIgnoreCase(usuarioValue)){

                }else{
                    if(usuarioC.length()>0){
                        if(dbh.updateSettings("usuario",usuarioValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el usuario", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }else{
                        if(dbh.onInsert("usuario",usuarioValue) == 1){

                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar el usuario", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }

                if(passC.equalsIgnoreCase(passValue)){
                    Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                    Intent opciones = new Intent(getApplicationContext(), Opciones.class);
                    startActivity(opciones);
                    finish();
                }else{
                    if(passC.length()>0){
                        if(dbh.updateSettings("password",passValue) == 1){
                            Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                            Intent main = new Intent(getApplicationContext(), Main.class);
                            startActivity(main);
                            finish();
                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar la contraseña", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }else{
                        if(dbh.onInsert("password",passValue) == 1){
                            Toast.makeText(getApplicationContext(), "Configuración guardada correctamente", Toast.LENGTH_LONG).show();
                            Intent main = new Intent(getApplicationContext(), Main.class);
                            startActivity(main);
                            finish();
                        }else{
                            Toast.makeText(getApplicationContext(), "Error al guardar la contraseña", Toast.LENGTH_LONG).show();
                            return;
                        }
                    }
                }
            }
        });
    }

    public void getParametros(){
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if(dbh.getSettings("service").length()>0)
                    txtService.setText(dbh.getSettings("service"));
                else
                    ;
                if(dbh.getSettings("usuario").length()>0)
                    txtUsuario.setText(dbh.getSettings("usuario"));
                else
                    ;
                if(dbh.getSettings("company").length()>0)
                    txtCompany.setText(dbh.getSettings("company"));
                else
                    ;
                if(dbh.getSettings("branch").length()>0)
                    txtBranch.setText(dbh.getSettings("branch"));
                else
                    ;
                txtPassword.setText(dbh.getSettings("password"));
            }
        });
    }
}
