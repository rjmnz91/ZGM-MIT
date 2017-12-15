package com.example.rodrigosanchez.zgm_mit;

/**
 * Created by Rodrigo Sanchez on 07/12/2017.
 */

public class Merchant {
    private String merchantName;
    private String merchantNum;

    public Merchant(){
    }

    public Merchant(String merchantName, String merchantNum){
        this.merchantName = merchantName;
        this.merchantNum = merchantNum;
    }


    public String getMerchantName() {
        return merchantName;
    }

    public void setMerchantName(String merchantName) {
        this.merchantName = merchantName;
    }

    public String getMerchantNum() {
        return merchantNum;
    }

    public void setMerchantNum(String merchantNum) {
        this.merchantNum = merchantNum;
    }

    @Override
    public String toString(){
        return merchantName;
    }

}
