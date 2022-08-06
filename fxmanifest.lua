client_script "nvhx.lua"
fx_version 'adamant'
games { 'gta5' }

client_script 'out/TattooStudio.net.dll'
server_script 'server.lua'

ui_page 'index.html'

files {
	'overlays.json',
	'out/Newtonsoft.Json.dll',
    'out/MenuAPI.dll',
    'storage/*'
}