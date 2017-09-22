package com.example.rodrigosanchez.zgm;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.widget.Toast;

/**
 * Created by Rodrigo Sanchez on 12/09/2017.
 */

public class DatabaseHandler extends SQLiteOpenHelper {

    private static final int DATABASE_VERSION = 1;

    private static final String DATABASE_NAME = "dbSettings";

    private static final String TABLE_SETTINGS = "Settings";

    private static final String KEY_OPTION = "option";
    private static final String KEY_VALUE = "value";

    public DatabaseHandler(Context context){
        super(context,DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_SETTINGS_TABLE = "CREATE TABLE " + TABLE_SETTINGS + "("+
                KEY_OPTION + " TEXT PRIMARY KEY," + KEY_VALUE + " TEXT);";
        db.execSQL(CREATE_SETTINGS_TABLE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_SETTINGS);
        onCreate(db);
    }

    long onInsert(String url){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_OPTION,"url");
        values.put(KEY_VALUE,url);
        long res = db.insert(TABLE_SETTINGS,null,values);
        db.close();
        return res;
    }

    public String getSettings(){
        try {
            String res = "";
            String select = "SELECT value FROM " + TABLE_SETTINGS + " WHERE " + KEY_OPTION + " = 'url'";
            SQLiteDatabase db = this.getWritableDatabase();
            Cursor cursor = db.rawQuery(select,null);
            if(cursor.moveToFirst()){
                do {
                    res = cursor.getString(0);
                }while(cursor.moveToNext());
            }
            return res;
        }catch(Exception e){
            return e.getMessage();
        }

    }

    public int updateSettings(String url){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_OPTION, "url");
        values.put(KEY_VALUE, url);
        int res = db.update(TABLE_SETTINGS,values,"option='url'",null);
        return res;
    }
}