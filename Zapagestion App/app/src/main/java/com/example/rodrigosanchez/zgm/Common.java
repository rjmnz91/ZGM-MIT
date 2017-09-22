package com.example.rodrigosanchez.zgm;

/**
 * Created by Rodrigo Sanchez on 31/08/2017.
 */

public class Common {
    private static String URL = "";

    public static String getURL(){
        return URL;
    }
    public static void setURL(String data){
        URL=data;
    }

    private static String homeURL = "";

    public static String getHomeURL(){
        return homeURL;
    }
    public static void setHomeURL(String data){
        homeURL=data;
    }
}
