# csharp-specflow-selenium

### Resources

* [SpecFlow](http://specflow.org/docs/) 
* [Selenium](https://www.seleniumhq.org/) 


# Environment setup

In Powershell run the following commands
 * Install scoop: 
 
   ```iex (new-object net.webclient).downloadstring('https://get.scoop.sh')```
 
 * Install nodejs: 
 
   ```scoop install nodejs```
  
 * Install Selenium-standalone: 
 
   ```npm install selenium-standalone -g```
 
   ```selenium-standalone install```
 
   ```selenium-standalone start```
 
# Setup - Windows
	Download and install Visual Studio

## Visual Studio Config
Open Visual Studio and configure the following;

### Install Add-ins


Tools => Extensions

* NUnit 3 Test Adapter
* Specflow flow Visual Studio
	
### Running
#### IDE
* Build
* Visual Studio: Tests => Windows => Test Explorer

#### Local Execution

```nunit3-console.exe "\pathToProject\Specflow.RestSharp.csproj" --where "cat=TagYouWantToRun"```
