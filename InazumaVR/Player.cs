using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO.Compression;

public class Player{
    static List<string> ProtaganistsSpecialCase = ["Matsukaze Tenma","Inamori Asuto","Sasanami Unmei","Endou Haru","Nishizono Shinsuke"];
    public string dubName {get; set;}
    public string dubNick {get;set;}
    public string subName {get;set;}
    public string subNick{get;set;}

    public string dubFirst {get; set;}
    public string dubLast{get; set;}

    public string gender{get;set;}

    public bool isMale {get => true ?  gender == "Male" : gender=="Female";
        set
        {
        if (value){
            gender = "Male";
        }
        else{
            gender = "Female";
        
        }}
        }
    
    public bool useGivenName {get; set;}

    public Player(){

    }
    public Player(string _dubName)
    {
        dubName = _dubName;
        string[] names = _dubName.Split(' ');
        dubNick = names [0];
        dubFirst = dubNick;
        if(names.Length >1){
            dubLast = names[1];
        }else{
            dubLast = null;
        }
        

    }

    public void setSubNames(string _subName){
        subName = _subName;
        if(ProtaganistsSpecialCase.Contains(subName)){
            this.useGivenName=true;
        }else{
            this.useGivenName=false;
        }
        if(subNick == null){
        string[] strs = _subName.Split(' ');
        if(strs.Length > 1 && (!isMale || useGivenName)){
                subNick = strs[1];
            
        }else{
            subNick = strs[0];
        }
        }
    }



}