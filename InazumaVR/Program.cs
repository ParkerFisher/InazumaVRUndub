using System.Runtime.InteropServices;
using CfgBinEditor.Level5.Binary;

public class Program
{
    public CfgBin cfgbin;
    
    public static string PLAYER_START = "Mark Evans";
    public static string PLAYER_END = "Kraken";

    public static string SKILL_START = "Fire Tornado";
    
    public static string SKILL_END = "Shot AT [CPASSIVE01]+3%[C] for same-element players";




    public void OpenFile(string filename){
        cfgbin = new CfgBin();
        cfgbin.Open(new FileStream(filename,FileMode.Open,FileAccess.Read));
    }

    public void GetPlayers(){
        string charFile = "/home/arch/InazumaElevenDecode/romfs/data/common/text/en/chara_text.cfg.bin";
        this.OpenFile(charFile);
        
        string[] strings = cfgbin.GetDistinctStrings();
        
        int index = 0;

        //traverse distinct strings until find Endou, he starts playerlist

        foreach(string str in strings){
           if(str == PLAYER_START){
            break;
           }
           index++;
        }
        List<Player> playerlist = new List<Player>();

        //as of Beta Demo, Kraken enemy is where playable characters end/
        for(;strings[index]!=PLAYER_END;index++ ){
            //Special Case for Ulvida, since Bellatrix redirects to an Orion Aliea Gakuen player
            if(strings[index]=="Bellatrix"){
                strings[index] = "Ulvida";
            }
            playerlist.Add(new Player(strings[index]));
        }
        
        
        //Player player = new Player("Clear");

        HttpParse parse = new HttpParse();
        foreach(Player player in playerlist){

        parse.getName(player);
        Console.WriteLine(player.dubName + " | " + player.dubNick + " | " +  player.subName+ " | "+player.subNick);
        cfgbin.ReplaceString(player.dubName,player.subName);
        cfgbin.ReplaceString(player.dubNick,player.subNick);
        cfgbin.ReplaceString(player.dubName,player.subName);
        cfgbin.ReplaceString(player.dubNick.ToUpper(),player.subNick.ToUpper());
        if(player.dubLast!=null){
        cfgbin.ReplaceString(player.dubLast,player.subNick);
        cfgbin.ReplaceString(player.dubLast.ToUpper(),player.subNick.ToUpper());
        }
        }

        strings = cfgbin.GetDistinctStrings();

        Database d = new Database();
        d.savePlayerFile(playerlist);

        //uncomment when ready to overwrite
        //cfgbin.Save();

    }

    public void GetSkills() {
         string charFile = "/home/arch/InazumaElevenDecode/romfs/data/common/text/en/skill_text.cfg.bin";
        this.OpenFile(charFile);

         string[] strings = cfgbin.GetDistinctStrings();
        
        int index = 0;

        //in demo, Fire Tornado is first skill
        foreach( string str in strings){
            Console.WriteLine(str);
            if(str == SKILL_START){
                break;
            }
            index++;
        }

        //find first passive skill 
        List<Skill> skillList = new List<Skill>();

        //as of Beta Demo, Kraken enemy is where playable characters end/
        for(;strings[index]!=SKILL_END;index++ ){
           if(strings[index].Contains('.')){
            strings[index] = strings[index].Replace(".",". ");
           }
            skillList.Add(new Skill(strings[index]));
        }
        
        HttpParse parse = new HttpParse();
        foreach(Skill skill in skillList){
            parse.getName(skill);

            cfgbin.ReplaceString(skill.dubName,skill.subName);

            
        }
         strings = cfgbin.GetDistinctStrings();
        
        foreach(string str in strings){
            Console.WriteLine(str);
        }
        Database d = new Database();
        d.saveSkillFile(skillList);


    }

    public void Test1(){
        System.Console.WriteLine("Hello, World!");
        Program program = new Program();
        program.OpenFile("/home/arch/InazumaElevenDecode/romfs/data/common/text/en/chara_text.cfg.bin");
        
        string[] strings = program.cfgbin.GetDistinctStrings();
        
        int index = 0;

        //traverse distinct strings until find Endou, he starts playerlist

        foreach(string str in strings){
           if(str == "Mark Evans"){
            break;
           }
           index++;
        }
        List<Player> playerlist = new List<Player>();

        //as of Beta Demo, Kraken enemy is where playable characters end/
        for(;strings[index]!="Kraken";index++ ){
            //Special Case for Ulvida, since Bellatrix redirects to an Orion Aliea Gakuen player
            if(strings[index]=="Bellatrix"){
                strings[index] = "Ulvida";
            }
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

        Database d = new Database();
        d.savePlayerFile(playerlist);
    }

    public void Test2(){
        HttpParse parse = new HttpParse();
        Player aphrodi = new Player("Byron Love");
        parse.getName(aphrodi);
        Console.WriteLine(aphrodi.dubName +" | " + aphrodi.dubNick + " | " + aphrodi.subName + " | " + aphrodi.subNick);
    }

    public void Test3(){
         Program program = new Program();
        program.OpenFile("/home/arch/InazumaElevenDecode/romfs/data/common/text/en/skill_text.cfg.bin");
        
        string[] strings = program.cfgbin.GetDistinctStrings();
        
        int index = 0;

        //in demo, Fire Tornado is first skill
        foreach( string str in strings){
            Console.WriteLine(str);
            if(str == "Fire Tornado"){
                break;
            }
            index++;
        }

        //find first passive skill 
  List<Skill> skillList = new List<Skill>();

        //as of Beta Demo, Kraken enemy is where playable characters end/
        for(;strings[index]!="Shot AT [CPASSIVE01]+3%[C] for same-element players";index++ ){
           if(strings[index].Contains('.')){
            strings[index] = strings[index].Replace(".",". ");
           }
            skillList.Add(new Skill(strings[index]));
        }
        
        HttpParse parse = new HttpParse();
        foreach(Skill skill in skillList){
            parse.getName(skill);

            program.cfgbin.ReplaceString(skill.dubName,skill.subName);

            
        }
         strings = program.cfgbin.GetDistinctStrings();
        
        foreach(string str in strings){
            Console.WriteLine(str);
        }
        Database d = new Database();
        d.saveSkillFile(skillList);
    }



    public static void Main(string[] args)
    {
        Program p = new Program();
        //p.Test1();
       // p.Test2();
        //p.Test3();
        p.GetPlayers();
        p.GetSkills();
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