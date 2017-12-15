package com.example.rodrigosanchez.zgm_mit;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.ImageButton;

public class Main extends AppCompatActivity  implements View.OnClickListener{

    private Button btnStart;
    private ImageButton ibtnSettings;
    DatabaseHandler dbh;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Set portrait orientation
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        // Hide title bar
        requestWindowFeature(Window.FEATURE_NO_TITLE);

        setContentView(R.layout.activity_main);
        btnStart = (Button)findViewById(R.id.btnIniciar);
        ibtnSettings = (ImageButton)findViewById(R.id.ibtnSettings);
        btnStart.setOnClickListener(this);
        ibtnSettings.setOnClickListener(this);
        dbh = new DatabaseHandler(getApplicationContext());
    }

    @Override
    public void onClick(View view) {
        switch(view.getId()){
            case R.id.btnIniciar:
                String settings = dbh.getSettings("url");
                Common.setOperationType("32");
                Common.setCountry("MEX");
                Common.setCurrency("MXN");
                Common.setPassword(dbh.getSettings("password"));
                Common.setUser(dbh.getSettings("usuario"));
                Common.setCompany(dbh.getSettings("company"));
                Common.setBranch(dbh.getSettings("branch"));
                Common.setService(dbh.getSettings("service"));
                if(settings.contains("http")) {
                    Common.setHomeURL(settings);
                    String newUrl = Common.getHomeURL() + "/Login.aspx";
                    Common.setURL(newUrl);
                    Intent main = new Intent(getApplicationContext(), Zapagestion.class);
                    startActivity(main);
                    finish();
                }else{
                    Intent main = new Intent(getApplicationContext(), Opciones.class);
                    startActivity(main);
                    finish();
                }
                break;
            case R.id.ibtnSettings:
                Intent setting = new Intent(getApplicationContext(),Opciones.class);
                startActivity(setting);
                finish();
                break;
        }
    }
}
