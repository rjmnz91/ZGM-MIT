package com.example.rodrigosanchez.zgm_mit;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.net.http.SslError;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.Handler;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.webkit.SslErrorHandler;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ImageButton;
import java.util.StringTokenizer;

/*
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
public class Zapagestion extends AppCompatActivity implements View.OnClickListener{

    private ImageButton ibtnRefresh;


    private ProgressDialog pd;
    private WebView webView;
    public static String codeBar = "";

    private static final boolean AUTO_HIDE = true;

    private static final int AUTO_HIDE_DELAY_MILLIS = 3000;

    private static final int UI_ANIMATION_DELAY = 300;
    private final Handler mHideHandler = new Handler();
    private View mContentView;
    private final Runnable mHidePart2Runnable = new Runnable() {
        @SuppressLint("InlinedApi")
        @Override
        public void run() {
            mContentView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_LOW_PROFILE
                    | View.SYSTEM_UI_FLAG_FULLSCREEN
                    | View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                    | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY
                    | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                    | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION);
        }
    };
    private View mControlsView;
    private final Runnable mShowPart2Runnable = new Runnable() {
        @Override
        public void run() {
            // Delayed display of UI elements
            ActionBar actionBar = getSupportActionBar();
            if (actionBar != null) {
                actionBar.show();
            }
            mControlsView.setVisibility(View.VISIBLE);
        }
    };
    private boolean mVisible;
    private final Runnable mHideRunnable = new Runnable() {
        @Override
        public void run() {
            hide();
        }
    };

    private final View.OnTouchListener mDelayHideTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View view, MotionEvent motionEvent) {
            if (AUTO_HIDE) {
                delayedHide(AUTO_HIDE_DELAY_MILLIS);
            }
            return false;
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_zapagestion);

        mVisible = true;
        mControlsView = findViewById(R.id.fullscreen_content_controls);
        mContentView = findViewById(R.id.fullscreen_content);
        pd = new ProgressDialog(this);
        setContentView(R.layout.activity_zapagestion);
        ibtnRefresh = (ImageButton) findViewById(R.id.ibtnRefresh);
        ibtnRefresh.setOnClickListener(this);
        webView= (WebView)findViewById(R.id.fullscreen_content);

        webView.setWebChromeClient(new WebChromeClient() {
            @Override
            public void onProgressChanged(WebView view, int progress) {
            }
        });

        webView.setWebViewClient(new WebViewClient() {
            public void onReceivedError(WebView view, int errorCode, String description, String failingUrl) {
                pd.setTitle("Cargando Página");
                pd.setMessage("Espere por favor...");
                pd.setCancelable(false);
                pd.show();
                view.loadUrl("blank");
                view.setVisibility(View.GONE);
                ibtnRefresh.setVisibility(View.VISIBLE);
            }

            @Override
            public void onPageStarted(WebView view, String url, Bitmap favicon) {
                pd.setTitle("Cargando Página");
                pd.setMessage("Espere por favor...");
                pd.setCancelable(false);
                pd.show();
                super.onPageStarted(view, url, favicon);
            }

            @Override
            public void onLoadResource(WebView view, String url) {
                url = url.replace("%24","$");
                url = url.replace("%2f","/");
                url = url.replace("%40","@");
                if(!url.contains("sendScanReader"))
                {
                }else if(!url.contains("Settings")) {

                }else if(!url.contains("Search")){

                }else{
                    view.loadUrl(Common.getURL());
                    if(pd.isShowing())
                        pd.dismiss();
                }
            }

            @Override
            public void onPageFinished(WebView view, String url) {
                //Toast.makeText(getApplicationContext(),"Carga Finalizada",Toast.LENGTH_LONG).show();
                if(pd.isShowing())
                    pd.dismiss();
                if(url.contains("sendScanReader"))
                {
                    Intent i = new Intent(getApplicationContext(),Scan.class);
                    //pd.dismiss();
                    startActivityForResult(i,100);

                }else if(url.contains("vta")){
                    StringTokenizer tokens = new StringTokenizer(url,"?");
                    String first = tokens.nextToken();
                    String second = tokens.nextToken();

                    StringTokenizer tokenValues = new StringTokenizer(second,"=");
                    //total a pagar durante la transaccion
                    String ttlPago = tokenValues.nextToken();
                    // referencia
                    String reference = tokenValues.nextToken();
                    // email
                    String email = tokenValues.nextToken();

                    Intent i = new Intent(getApplicationContext(),Payment.class);
                    i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    i.putExtra("ttlPago",ttlPago);
                    i.putExtra("vtaRef",reference);
                    i.putExtra("correo",email);
                    //view.loadUrl(Common.getHomeURL());
                    //pd.dismiss();
                    startActivityForResult(i,100);
                }
                else{
                    if(pd.isShowing())
                        pd.dismiss();
                    Common.setURL(url);
                }
                if(pd.isShowing())
                    pd.dismiss();
                super.onPageFinished(view, url);
                if(pd.isShowing())
                    pd.dismiss();
            }

            @Override
            public void onReceivedSslError(WebView view, SslErrorHandler handler, SslError error) {
                handler.proceed();
            }
        });

        webView.getSettings().setJavaScriptEnabled(true);
        webView.getSettings().setDomStorageEnabled(true);

        webView.setVerticalScrollBarEnabled(false);

        webView.setHorizontalScrollBarEnabled(false);

        webView.loadUrl(Common.getURL());

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        WebView webview = (WebView) findViewById(R.id.fullscreen_content);
        String newURL = Common.getURL();
        webview.loadUrl(newURL);
    }

    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        delayedHide(100);
    }

    private void toggle() {
        if (mVisible) {
            hide();
        } else {
            show();
        }
    }

    private void hide() {
        // Hide UI first
        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.hide();
        }
        mControlsView.setVisibility(View.GONE);
        mVisible = false;

        mHideHandler.removeCallbacks(mShowPart2Runnable);
        mHideHandler.postDelayed(mHidePart2Runnable, UI_ANIMATION_DELAY);
    }

    @SuppressLint("InlinedApi")
    private void show() {
        // Show the system bar
        mContentView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION);
        mVisible = true;

        mHideHandler.removeCallbacks(mHidePart2Runnable);
        mHideHandler.postDelayed(mShowPart2Runnable, UI_ANIMATION_DELAY);
    }

    private void delayedHide(int delayMillis) {
        mHideHandler.removeCallbacks(mHideRunnable);
        mHideHandler.postDelayed(mHideRunnable, delayMillis);
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        // ignore orientation/keyboard change
        super.onConfigurationChanged(newConfig);
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        WebView webView = (WebView) findViewById(R.id.fullscreen_content);
        if (event.getAction() == KeyEvent.ACTION_DOWN) {
            switch (keyCode) {
                case KeyEvent.KEYCODE_BACK:
                    if (webView.canGoBack()) {
                        webView.goBack();
                    } else {
                        finish();
                    }
                    return true;
            }

        }
        return super.onKeyDown(keyCode, event);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.ibtnRefresh:
                webView.loadUrl(Common.getURL());
                ibtnRefresh.setVisibility(view.GONE);
                webView.setVisibility(view.VISIBLE);
                break;
        }
    }
}