# Crystal-Launcher
An auto updater and launcher for game clients.

**Using The Source**

**Step #1:**
*Navigate here.*

![Navigate To](https://i.imgur.com/xEzOm6f.png)



*Edit this Line to where you hold your UpdateInfo.dat file generated with the Update Maker*



![Edit](https://i.imgur.com/cWsO5AL.png)



**Step #2:** *Put all files including directories in a folder called Files in the same folder with the update maker, and turn on the update maker*

Your structure should look something like this.



![Folder Structure](https://i.imgur.com/fwIyYk9.png)

**Step #3:**
*Open the Update Maker and it should auto populate with the files in your Files directory like below*



![Update Maker Generation1](https://i.imgur.com/gw1dtvJ.png)


**Step #4:**
*Place your current version number of the assembly in the Version field.*
**NOTE: All updates that you publish, you will need to include a new version of the launcher with a version (In the compiled Launcher's Assembly Version) that matches the the version number generated in the UpdateInfo.dat**
**Just make sure your launcher assembly version matches the update. you can do that by opening the Launcher project properties**


![Update Maker Generation2](https://i.imgur.com/XQVbT0Z.png)

*Hit generate and you will receive an UpdateInfo.dat file*
*You guessed it. This goes in the same url as the line you edited in step 1*

*Now just upload all your files without any folders on your server in the same directory as your UpdateInfo.dat*



*Like this*


![Online File Structure](https://i.imgur.com/CMZIWoW.png)



*Revert the local Launchers Assembly version to a lower version number and compile*

*Run the launcher from your client directory to update.*
