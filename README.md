## This application is designed for www.renaissancekingdoms.com.

App parse player profiles and save to txt file obtained data.

Befor first start you must create config.txt file in main folder when .exe file.

Example config.txt:

```
PHPSESSID= Your PHPSESSID from cookies browers.
UrlProfile=https://www.krolestwa.com/FichePersonnage.php?login=
UrlGame=https://www.krolestwa.com/
PathPlayer= path to player list file
PathSave= path when app save result
```
UrlProfile and UrlGame must be like Example. Nowadays app work only Polish url.

App save result in one file per day overwriting last file of the day.  

Player list file:

This app use ","(Comma) to separet between players. Other char to separet are not supported.

