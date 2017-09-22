package com.example.rodrigosanchez.zgm;

import android.annotation.SuppressLint;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.Handler;
import android.view.MotionEvent;
import android.view.View;

import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.view.Window;
import android.widget.Button;
import android.widget.ImageButton;

/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
public class SplashLoad extends AppCompatActivity implements View.OnClickListener{
    /**
     * Whether or not the system UI should be auto-hidden after
     * {@link #AUTO_HIDE_DELAY_MILLIS} milliseconds.
     */
    private static final long SPLASH_SCREEN_DELAY = 3000;

    private static final boolean AUTO_HIDE = true;

    /**
     * If {@link #AUTO_HIDE} is set, the number of milliseconds to wait after
     * user interaction before hiding the system UI.
     */
    private static final int AUTO_HIDE_DELAY_MILLIS = 3000;

    /**
     * Some older devices needs a small delay between UI widget updates
     * and a change of the status and navigation bar.
     */
    private static final int UI_ANIMATION_DELAY = 300;

    private View mContentView;
    private View mControlsView;
    private Button btnStart;
    private ImageButton ibtnSettings;
    private boolean mVisible;
    DatabaseHandler dbh;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Set portrait orientation
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        // Hide title bar
        requestWindowFeature(Window.FEATURE_NO_TITLE);

        setContentView(R.layout.activity_splash_load);
        btnStart = (Button)findViewById(R.id.btnIniciar);
        ibtnSettings = (ImageButton)findViewById(R.id.ibtnSettings);
        btnStart.setOnClickListener(this);
        ibtnSettings.setOnClickListener(this);
        dbh = new DatabaseHandler(getApplicationContext());

        TimerTask task = new TimerTask() {
            @Override
            public void run() {

                // Start the next activity
                //Intent mainIntent = new Intent().setClass(getApplicationContext(), Main.class);
                //startActivity(mainIntent);

                // Close the activity so the user won't able to go back this
                // activity pressing Back button
                //finish();
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        btnStart.setVisibility(View.VISIBLE);
                    }
                });
            }
        };

        // Simulate a long loading process on application startup.
        Timer timer = new Timer();
        timer.schedule(task, SPLASH_SCREEN_DELAY);
    }

    @Override
    public void onClick(View view) {
        switch(view.getId()){
            case R.id.btnIniciar:
                String settings = dbh.getSettings();
                if(settings.contains("http")) {
                    Common.setHomeURL(settings);
                    //Common.setHomeURL(settings);
                    String newUrl = Common.getHomeURL() + "/Login.aspx";
                    Common.setURL(newUrl);
                    Intent main = new Intent(getApplicationContext(), Main.class);
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
