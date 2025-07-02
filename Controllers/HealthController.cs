using Microsoft.AspNetCore.Mvc;

namespace FidelityTesting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        // Basic health check - JSON
        [HttpGet(Name = "GetHealth")]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Message = "All systems operational! 🚀",
                Version = "1.0.0"
            });
        }

        // Simple status - text
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok("Healthy ✅");
        }

        // Cute HTML dashboard
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            var currentTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            var machineName = Environment.MachineName;
            var memoryMB = (Environment.WorkingSet / 1024 / 1024).ToString("N0");
            
            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Health Dashboard</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #74b9ff, #0984e3);
            margin: 0;
            padding: 20px;
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }}
        .container {{
            background: white;
            border-radius: 20px;
            padding: 40px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.1);
            text-align: center;
            max-width: 500px;
            width: 100%;
        }}
        .status {{
            font-size: 4rem;
            margin-bottom: 20px;
        }}
        .title {{
            font-size: 2rem;
            color: #2d3436;
            margin-bottom: 10px;
            font-weight: bold;
        }}
        .subtitle {{
            color: #636e72;
            font-size: 1.2rem;
            margin-bottom: 30px;
        }}
        .info-box {{
            background: #f8f9fa;
            border-radius: 15px;
            padding: 20px;
            margin: 20px 0;
            text-align: left;
        }}
        .info-item {{
            display: flex;
            justify-content: space-between;
            margin: 10px 0;
            padding: 5px 0;
        }}
        .label {{
            font-weight: bold;
            color: #2d3436;
        }}
        .value {{
            color: #00b894;
            font-weight: bold;
        }}
        .buttons {{
            margin-top: 30px;
        }}
        .btn {{
            background: #00b894;
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 25px;
            text-decoration: none;
            display: inline-block;
            margin: 5px;
            font-weight: bold;
            transition: all 0.3s ease;
        }}
        .btn:hover {{
            background: #00a085;
            transform: translateY(-2px);
        }}
        .footer {{
            margin-top: 30px;
            color: #636e72;
            font-size: 0.9rem;
        }}
        @keyframes bounce {{
            0%, 100% {{ transform: translateY(0); }}
            50% {{ transform: translateY(-10px); }}
        }}
        .bounce {{
            animation: bounce 2s infinite;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='status bounce'>&#9989;</div>
        <div class='title'>System Healthy!</div>
        <div class='subtitle'>All systems are running smoothly</div>
        
        <div class='info-box'>
            <div class='info-item'>
                <span class='label'>Status:</span>
                <span class='value'>Operational</span>
            </div>
            <div class='info-item'>
                <span class='label'>Time:</span>
                <span class='value'>{currentTime} UTC</span>
            </div>
            <div class='info-item'>
                <span class='label'>Server:</span>
                <span class='value'>{machineName}</span>
            </div>
            <div class='info-item'>
                <span class='label'>Memory:</span>
                <span class='value'>{memoryMB} MB</span>
            </div>
            <div class='info-item'>
                <span class='label'>Version:</span>
                <span class='value'>1.0.0</span>
            </div>
        </div>

        <div class='buttons'>
            <a href='/health' class='btn'>JSON Health</a>
            <a href='/WeatherForecast' class='btn'>Weather API</a>
            <a href='/health/status' class='btn'>Quick Status</a>
        </div>

        <div class='footer'>
            <div>FidelityTesting Health Monitor</div>
            <div>Auto-refresh in <span id='countdown'>30</span> seconds</div>
        </div>
    </div>

    <script>
        let countdown = 30;
        const timer = setInterval(() => {{
            countdown--;
            document.getElementById('countdown').textContent = countdown;
            if (countdown <= 0) {{
                window.location.reload();
            }}
        }}, 1000);
        
        console.log('Health dashboard loaded successfully!');
    </script>
</body>
</html>";

            return Content(html, "text/html");
        }
    }
}