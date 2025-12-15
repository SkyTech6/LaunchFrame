# LaunchFrame

<p align="center">
  <img src=".github/images/launchframe.png" alt="LaunchFrame" width="480" />
</p>

LaunchFrame is a Windows launcher that starts Warframe together with helper tools (AlecaFrame, Overframe currently) and opens your reference links in one click (such as wiki.warframe or warframe.market).

## What it does
- Starts helpers first, waits for them to be ready, then launches Warframe (default Steam URI `steam://rungameid/230410`).
- Lets you enable/disable helpers and URLs, edit paths/arguments, and save settings.
- Saves config at `%AppData%\LaunchFrame\config.json` and auto-discovers common install paths when missing.
- Can run headless with `--oneclick` for shortcut/Task Scheduler use.

## Requirements
- Windows 10/11

## Setup
1. Download the latest release zip from GitHub Releases.
2. Extract it anywhere (e.g., Desktop or a tools folder).
3. Run `LaunchFrame.exe`.

## Using LaunchFrame
- Set paths/arguments for AlecaFrame and Overframe, toggle them on/off.
- Set the Warframe URI if you use a non-default launcher.
- Optional: check "Wait for helper readiness" or "Skip if already running".
- Click "Launch Session" to run the sequence.
- Use "Create One-Click" to generate a shortcut that runs `--oneclick` without the UI.

## Notes
- No game injection or modification; LaunchFrame only starts processes and opens URLs.
- Config is created on first save/launch; edit via the UI or by editing the JSON if needed.

## Support LaunchFrame

Enjoying the utility of LaunchFrame? Consider supporting the developer: 

**Buy / Play My Games!** 

<p align="center">
    <a href="https://store.steampowered.com/app/713740/Train_Your_Minibot/">
        <img src="https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/713740/header.jpg" alt="TrainYourMinibot" width="250"/>
    </a>
    <a href="https://store.steampowered.com/app/1792500/Boring_Movies/">
        <img src="https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1792500/header.jpg?t=1754577490" alt="BoringMovies" width="250"/>
    </a>
    <a href="https://store.steampowered.com/app/1490570/git_gud/">
        <img src="https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1490570/88fe8479154764080427ee0b703b2b72447740a4/header.jpg?t=1757900982" alt="gitgud" width="250"/>
    </a>
    <a href="https://store.steampowered.com/app/3819510/DexSweeper/">
        <img src="https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/3819510/6d92a431630be001667a7da561938d84714264c2/header.jpg?t=1757724028" alt="DexSweeper" width="250"/>
    </a>
</p>

Donations Accepted

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/skytech6)
