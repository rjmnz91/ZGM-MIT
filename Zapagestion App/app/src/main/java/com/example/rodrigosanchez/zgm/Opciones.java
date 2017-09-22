package com.example.rodrigosanchez.zgm;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.io.File;


public class Opciones extends AppCompatActivity implements View.OnClickListener{

    private Button btnSave;
    private EditText txtUrl;
    DatabaseHandler dbh;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_opciones);
        dbh = new DatabaseHandler(getApplicationContext());
        txtUrl = (EditText) findViewById(R.id.txtUrl);
        btnSave = (Button) findViewById(R.id.btnGuardar);
        //btnSave.setOnClickListener(this);
        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String settings = dbh.getSettings();
                if(settings.contains("http"))
                    dbh.updateSettings(txtUrl.getText().toString());
                else
                    dbh.onInsert(txtUrl.getText().toString());
                Common.setHomeURL(settings);
                Toast.makeText(getApplicationContext(),"Configuración guardada correctamente",Toast.LENGTH_LONG).show();
                Intent main = new Intent(getApplicationContext(), SplashLoad.class);
                startActivity(main);
                finish();
            }
        });
        getParametros();
    }

    public void getParametros(){
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                txtUrl.setText(dbh.getSettings());
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
}
