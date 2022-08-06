using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace TattooStudioServer
{
    public class Main : BaseScript
    {
        public static dynamic ESX;
		private string content;
        public Main()
        {
            TriggerEvent("esx:getSharedObject", new Action<dynamic>((response) =>
            {
                ESX = response;
            }));
			content = LoadResourceFile(GetCurrentResourceName(), "overlays.json");
            EventHandlers["tattoo:getJson"] += new Action<Player>(OnEvent);
            EventHandlers["tattoo:getData"] += new Action<Player>(OnData);
            EventHandlers["tattoo:setData"] += new Action<Player, string, string>(SetData);
        }

        private void SetData([FromSource] Player player, string obj, string remove)
        {
            var xPlayer = ESX.GetPlayerFromId(player.Handle);
            if(remove == "remove")
            {
                if (xPlayer.getMoney() >= 5000)
                {
                    xPlayer.removeMoney(5000);
                    xPlayer.showNotification("Dir wurden deine Tattoos erfolgreich entfernt!");
                    string[] pars = 
                    {
                        obj,
                        xPlayer.identifier
                    };
                    MySQLHandler.Instance.Execute("UPDATE tattoos SET data= ? WHERE identifier= ?", pars, new Action<dynamic>((_) => {

                    }));
                    TriggerClientEvent(player, "tattoo:apply");
                }
                else
                {
                    xPlayer.showNotification("Du hast nicht genug Geld! Das entfernen kostet 5000€");
                    TriggerClientEvent(player, "tattoo:apply");
                }
            } else
            {
                if (xPlayer.getMoney() >= 5000)
                {
                    xPlayer.removeMoney(5000);
                    xPlayer.showNotification("Die Tattoos wurden erfolgreich gestochen!");
                    string[] pars = {
                        obj,
                        xPlayer.identifier
                    };
                    MySQLHandler.Instance.Execute("UPDATE tattoos SET data= ? WHERE identifier= ?", pars, new Action<dynamic>((_) => {

                    }));
                }
                else
                {
                    xPlayer.showNotification("Du hast nicht genug Geld! Du brauchst 5000€");
                    TriggerClientEvent(player, "tattoo:apply");
                }
            }
           
        }

        private void OnEvent([FromSource] Player player)
        {
            TriggerClientEvent(player, "tattoo:sendJson", content);
        }

        private void OnData([FromSource] Player player)
        {
            var xPlayer = ESX.GetPlayerFromId(player.Handle);
            string[] pars = 
            {
                xPlayer.identifier
            };
            MySQLHandler.Instance.FetchAll("SELECT * FROM tattoos WHERE identifier= ?", pars, new Action<List<dynamic>>((objs) => {
                if(objs.Count == 0)
                {
                    string[] newpars = 
                    {
                        xPlayer.identifier,
                    };
                    MySQLHandler.Instance.Execute("INSERT INTO tattoos SET identifier= ?", newpars, new Action<dynamic>((_) => { }));
                    TriggerClientEvent(player, "tattoo:sendData", "");
                }
                else
                {
                    TriggerClientEvent(player, "tattoo:sendData", objs[0].data);
                }
            }));
        }
    }
}
