using System.Diagnostics.Contracts;
using System.Globalization;

public class Player{
    public string dubName {get; set;}
    public string dubNick {get;set;}
    public string subName {get;set;}
    public string subNick{get;set;}

    public string dubFirst {get;}
    public string dubLast{get;}

    public bool isMale {get;set;}

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
        if(subNick == null){
        string[] strs = _subName.Split(' ');
        if(strs.Length > 1 && !isMale){
                subNick = strs[1];
            
        }else{
            subNick = strs[0];
        }
        }
    }

}