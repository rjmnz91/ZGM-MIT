package com.example.rodrigosanchez.zgm_mit;

/**
 * Created by Rodrigo Sanchez on 06/11/2017.
 */

public class Common {
    private static String URL  =  "";
    private static String homeURL  =  "";
    private static String service  =  "";
    private static String user =  "";
    private static String password  =  "";
    private static String branch =  "";
    private static String company  =  "";
    private static String merchant  =  "";
    private static String operationType  =  "";
    private static String country  =  "";
    private static String currency  =  "";

    public static String getURL(){
        return URL;
    }
    public static void setURL(String data){
        URL = data;
    }

    public static String getHomeURL(){
        return homeURL;
    }
    public static void setHomeURL(String data){
        homeURL = data;
    }

    public static String getService(){
        return service;
    }
    public static void setService(String data){
        service = data;
    }

    public static String getUser(){
        return user;
    }
    public static void setUser(String data){
        user = data;
    }

    public static String getPassword(){
        return password;
    }
    public static void setPassword(String data){
        password = data;
    }

    public static String getBranch(){
        return branch;
    }
    public static void setBranch(String data){
        branch = data;
    }

    public static String getCompany(){
        return company;
    }
    public static void setCompany(String data){
        company = data;
    }

    public static String getMerchant(){
        return merchant;
    }
    public static void setMerchant(String data){
        merchant = data;
    }

    public static String getOperationType(){
        return operationType;
    }
    public static void setOperationType(String data){
        operationType = data;
    }

    public static String getCountry(){
        return country;
    }
    public static void setCountry(String data){
        country = data;
    }
    
    public static String getCurrency(){
        return currency;
    }
    public static void setCurrency(String data){
        currency = data;
    }
}
