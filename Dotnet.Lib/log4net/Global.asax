var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config"); 
log4net.Config.XmlConfigurator.Configure(fileInfo);
