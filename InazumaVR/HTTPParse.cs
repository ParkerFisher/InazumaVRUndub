using System.Net.NetworkInformation;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
class HttpParse{

    public const string baseURL = "https://inazuma-eleven.fandom.com/wiki/";
    public HttpParse(){
        
    }

    public static string GetFinalRedirect(string url)
{
    if(string.IsNullOrWhiteSpace(url))
        return url;

    int maxRedirCount = 8;  // prevent infinite loops
    string newUrl = url;
    do
    {
        HttpWebRequest req = null;
        HttpWebResponse resp = null;
        try
        {
            req = (HttpWebRequest) HttpWebRequest.Create(url);
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;
            resp = (HttpWebResponse)req.GetResponse();
            switch (resp.StatusCode)
            {
                case HttpStatusCode.OK:
                    return newUrl;
                case HttpStatusCode.Redirect:
                case HttpStatusCode.MovedPermanently:
                case HttpStatusCode.RedirectKeepVerb:
                case HttpStatusCode.RedirectMethod:
                    newUrl = resp.Headers["Location"];
                    if (newUrl == null)
                        return url;

                    if (newUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
                    {
                        // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                        Uri u = new Uri(new Uri(url), newUrl);
                        newUrl = u.ToString();
                    }
                    break;
                default:
                    return newUrl;
            }
            url = newUrl;
        }
        catch (WebException)
        {
            // Return the last known good URL
            return newUrl;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            if (resp != null)
                resp.Close();
        }
    } while (maxRedirCount-- > 0);

    return newUrl;
}
   

    public bool HasNickname(Player player,HtmlDocument document){
        HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@data-source='nickname_dub']");
        if(node!=null){
            player.dubNick = document.DocumentNode.SelectSingleNode("//div[@data-source='nickname_dub']/div").InnerText.Split(' ')[0];
        }
        return node == null ? false :true;
    }

    public bool IsMale(HtmlDocument document){
        //if they are male, nickname is family name in most cases, and female is given name
        HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@data-source='gender']");
        return node != null && node.InnerText.Contains("Female") ? false : true;
       
    }

    public void getName(Player player){
        string currentURL = baseURL + player.dubName.Replace(" ","_");
        string newURL = GetFinalRedirect(currentURL);
        string subName = newURL.Replace(baseURL,"").Replace('_',' ');
        
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(baseURL+player.dubName.Replace(" ","_"));
        
        
        if(HasNickname(player,document) ){
            //get "also known as <name> d
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/b");
            if(nodes.Count>1){
                if(player.dubName == player.dubNick ){
                    subName = nodes[1].InnerText;
                }else{
            player.subNick = nodes[1].InnerText;
                }
            }
        }
        player.isMale = IsMale(document);
        player.setSubNames(subName);

       
       
    }

    public void getName(Skill skill){
         string currentURL = baseURL + skill.dubName.Replace(" ","_");
        string newURL = GetFinalRedirect(currentURL);
        string subName = newURL.Replace(baseURL,"").Replace('_',' ');

        skill.subName = subName;
    }
}