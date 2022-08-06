ESX = nil

TriggerEvent('esx:getSharedObject', function(obj) ESX = obj end)

RegisterServerEvent("tattoo:setData", function(remove)
    local xPlayer = ESX.GetPlayerFromId(source)
    if xPlayer.getMoney() >= 5000 then
        xPlayer.removeMoney(5000)
        xPlayer.triggerEvent("tattoo:apply", true)
        if remove then
            xPlayer.showNotification("Dir wurden deine Tattoos erfolgreich entfernt!")
        else
            xPlayer.showNotification("Die Tattoos wurden erfolgreich gestochen!")
        end
    else
        xPlayer.showNotification("Du hast nicht genug Geld! Du brauchst 5000€")
        xPlayer.triggerEvent("tattoo:apply", false)
    end
end)
