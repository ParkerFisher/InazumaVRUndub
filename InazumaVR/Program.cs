using System.Runtime.InteropServices;
using CfgBinEditor.Level5.Binary;

public class Program
{
    public CfgBin cfgbin;
    public void OpenFile(string filename){
        cfgbin = new CfgBin();
        cfgbin.Open(new FileStream(filename,FileMode.Open,FileAccess.Read));
    }

    public void Test1(){
        System.Console.WriteLine("Hello, World!");
        Program program = new Program();
        program.OpenFile("/home/arch/InazumaElevenDecode/romfs/data/common/text/en/chara_text.cfg.bin");
        
        string[] strings = program.cfgbin.GetDistinctStrings();
        
        int index = 0;
        foreach(string str in strings){
           if(str == "Mark Evans"){
            break;
           }
           index++;
        }
        List<Player> playerlist = new List<Player>();
        for(;strings[index]!="Kraken";index++ ){
            playerlist.Add(new Player(strings[index]));
        }
        
        
        //Player player = new Player("Clear");

        HttpParse parse = new HttpParse();
        foreach(Player player in playerlist){
        parse.getName(player);
        Console.WriteLine(player.dubName + " | " + player.dubNick + " | " +  player.subName+ " | "+player.subNick);
        program.cfgbin.ReplaceString(player.dubName,player.subName);
        program.cfgbin.ReplaceString(player.dubNick,player.subNick);
        program.cfgbin.ReplaceString(player.dubLast,player.subNick);
        program.cfgbin.ReplaceString(player.dubName,player.subName);
        program.cfgbin.ReplaceString(player.dubNick.ToUpper(),player.subNick.ToUpper());
        if(player.dubLast!=null){
        program.cfgbin.ReplaceString(player.dubLast.ToUpper(),player.subNick.ToUpper());
        }
        }

        strings = program.cfgbin.GetDistinctStrings();
        
        foreach(string str in strings){
            Console.WriteLine(str);
        }
    }

    public void Test2(){
        HttpParse parse = new HttpParse();
        Player aphrodi = new Player("Byron Love");
        parse.getName(aphrodi);
        Console.WriteLine(aphrodi.dubName +" | " + aphrodi.dubNick + " | " + aphrodi.subName + " | " + aphrodi.subNick);
    }
    public static void Main(string[] args)
    {
        Program p = new Program();
        p.Test1();
       // p.Test2();


       /*
       *    Protaganists need special nickname treatment, Tenma, Asuto, and Unmei go by first name
            Implement check for HasNickname for dub Shinsuke/JP
            fix Alien nicknames
            
       *
       *
       *
       *
       */
        
    }
}