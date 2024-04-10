using TempArAn.Domain.Exceptions.SourceExeptions.HtmlSourceExceptions;
using TempArAn.Domain.Models.Source;

namespace TempArAn.Tests.Tests
{
    public class SourceTests
    {
        public static string page = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>ESP0005F87A</title><meta http-equiv=\"REFRESH\" content=\"60\"><meta name=\"viewport\" content=\"width=480\" /><meta name=\"mobile-web-app-capable\" content=\"yes\" /><link rel=\"stylesheet\" href=\"main.css\"></head><body><br><div style=\"text-align: center\"><div style=\"display: inline-block\"><div class=\"name fll\">ESP0005F87A<div class=\"www\">MaksMS <a href=\"http://wifi-iot.com\" target=\"_blank\">wifi-iot.com</a><br> Pro mode</div></div><div class=\"spV2 fll\"></div><div class=\"spV fll\"></div><div class=\"spV2 fll\"></div><div class=\"sys fll\">Free memory: 17008 B.<br>Uptime: 5 day 20:25:50<br>VDD: 3652 mV. WIFI: -80 dBm.<br>Updated: 00:20:17 06.01.2023<br>Local Time: 20:25:50 5.00.0 Sa<br></div></div></div><div class=\"c2\" ><div class=\"h\" style=\"background: #7D8EE2\">Sensors:</div><div class=\"c\"><b>1-Wire DS18B20:</b><br>1: 2.8 &deg;C. 2: 1.9 &deg;C. <br><hr><B>BH1750</B>: Light: 1750 lux<hr><B>AHT10</B>: <br>Temperature: 6.36 &deg;C, Humidity: 53.59 %<hr><table width=\"100%\"></table><hr></div><br><div class=\"h\" style=\"background: #73c140\">GPIO:</div><div class=\"c\" style=\"padding-bottom: 28px\"><script type=\"text/javascript\" src=\"js.js\"></script></div><br><div class=\"h\" style=\"background:#808080\">Config:</div><div class=\"c\"> <a href=\"configmain\">Main</a> <a href=\"configall\">Hardware</a> <a href=\"configsrv\">Servers</a> <a href=\"configpio\">GPIO</a> <a href=\"config1wire\">1-wire</a> <a href=\"configscheduler\">Scheduler</a> <a href=\"configtermo\">Thermostat</a> <a href=\"config2logic\">Logics2</a> <a href=\"configtcp\">TCP/UDP_client</a> <a href=\"configinter\">Interpreter</a> <a href=\"configkeys\">GPIO_Keys</a> <a href=\"configd2d\">D2D</a><hr><a href=\"/i2cscan\">I2C_Scanner</a> <a href=\"/configupd\">Firmware_update</a> <a href=\"/debug\">Debug</a> <a href=\"/configrst\">Restart</a> <a href=\"configopt\">Import/Export</a> <a href=\"listsens\">Metrics</a></div></body></html>";
        public static string wrongPage = "wrongPage";
        public static HTMLSource s = new HTMLSource("a", "a", "Temperature: ", " &deg;", Guid.NewGuid(), 5);
        [Fact]
        public void HTMLSourceParseTest_Success()
        {
            Assert.True(s.GetValueFromPage(page) - 6.36 < 0.1);
        }

        [Fact]
        public void HTMLSourceParseTest_Faulure()
        {
            Assert.Throws<ParseErrorHtmlSourceException>(() => s.GetValueFromPage(wrongPage));
        }
        [Fact]

        public async Task HTMLSourceGetTest_Faulure()
        {
            await Assert.ThrowsAsync<NotFoundHtmlSourceException>(async () => await s.GetPageAsync());
        }
    }
}
