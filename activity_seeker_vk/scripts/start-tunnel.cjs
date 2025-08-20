const { exec } = require("child_process");
const fs = require("fs");

const tunnelCommand = "vk-tunnel --host=localhost --port=5173";

const tunnel = exec(tunnelCommand);

tunnel.stdout.on("data", (data) => {
  const text = data.toString();
  console.log(text);

  const match = text.match(/https:\/\/[a-z0-9-]+\.vk-tunnel\.com\/?/);
  if (match) {
    const url = match[0];
    fs.writeFileSync(".vk-tunnel-url", url);
    console.log(`✅ vktunnel URL сохранён: ${url}`);
  }
});

tunnel.stderr.on("data", (data) => {
  console.error(data.toString());
});

tunnel.on("close", (code) => {
  console.log(`vk-tunnel завершился с кодом ${code}`);
});