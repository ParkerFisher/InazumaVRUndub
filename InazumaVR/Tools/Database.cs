using System.ComponentModel;

class Database{
    List<string> playerList;

    public void LoadFile(string filename){
        using var reader = new StreamReader(filename);
        string line;
        // Read and display lines from the file until the end of
        // the file is reached.
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }

    }

    public void savePlayerFile(List<Player> players){
        using var writer = new StreamWriter("../../../data/players.csv");
        writer.WriteLine("Dub Name, Dub First, Dub Last , Sub Name, Sub Nickname, Gender, isSpecialCase");
        foreach (Player player in players){
            if(player.dubLast == ""){
                player.dubLast = "N/A";
            }
            writer.WriteLine(player.dubName + ","+ player.dubFirst + "," + player.dubLast + "," + player.subName + "," +player.subNick + "," + player.gender + "," + player.useGivenName );
        }
    }

        public void saveSkillFile(List<Skill> skills){
        using var writer = new StreamWriter("../../../data/skills.csv");
        writer.WriteLine("Dub Name, Sub Name");
        foreach (Skill skill in skills){
            writer.WriteLine(skill.dubName +","+ skill.subName  );
        }
    }

    public Player[] getData(){

        return new Player[3];
    }
}