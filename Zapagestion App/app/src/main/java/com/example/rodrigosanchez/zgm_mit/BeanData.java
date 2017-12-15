package com.example.rodrigosanchez.zgm_mit;

/**
 * Created by Rodrigo Sanchez on 06/11/2017.
 */

public class BeanData implements BeanControllerData {
    private String user;
    private String pwd;
    private String branch;
    private String company;
    private String merchant;
    private String operationType;
    private String country;
    private String currency;

    /**Development*/
    //private String server = "https://dev10.mitec.com.mx";
    private String server = "https://qa10.mitec.com.mx";
    //private String server = "https://mpos.mitec.com.mx";

    public BeanData() {

		/*Retail*/
        if(retail){
            user = "9249GGCA1";
            //pwd = "JUNIO2017";
            pwd = "2EFGNG0HZ0";
            branch = "0113";
            company= "9249";
            merchant = "";
            operationType = "32";
            country = "MEX";
            //merchant para pago de contado
        }

    }


    public String getUser() {
        return user;
    }

    public void setUser(String user) {
        this.user = user;
    }

    public String getPwd() {
        return pwd;
    }

    public void setPwd(String pwd) {
        this.pwd = pwd;
    }

    public String getBranch() {
        return branch;
    }

    public void setBranch(String branch) {
        this.branch = branch;
    }

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    public String getServer() {
        return server;
    }

    public void setServer(String server) {
        this.server = server;
    }

    public String getMerchant() {
        return merchant;
    }

    public void setMerchant(String merchant) {
        this.merchant = merchant;
    }

    public String getOperationType() {
        return operationType;
    }

    public void setOperationType(String operationType) {
        this.operationType = operationType;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getCurrency() {
        return currency;
    }

    public void setCurrency(String currency) {
        this.currency = currency;
    }

    @Override
    public String toString() {
        return "BeanData [user=" + user + ", pwd=" + pwd + ", branch=" + branch
                + ", company=" + company + ", merchant=" + merchant
                + ", server=" + server + "]";
    }

}