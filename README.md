# Crasher Unlocker V1.2

Offline Castle Crashers save editor for your local Steam profile.

## Features

- Character unlocks
- Level, XP, Gold, and supported stats
- Weapons and Animal Orbs
- Consumables and equipment
- Story/progression editing
- Profile unlocks
- Character balancing and MAX options
- Automatic backups and restore
- Light and Dark themes

## Supported limits

- Level: 1–99
- Strength: 1–25
- Defense: 1–25
- Magic: 1–25
- Agility: 1–25

Crasher Unlocker intentionally does not expose over-limit character values that Castle Crashers clamps or rejects.

## Safety

Crasher Unlocker edits only your own local save/profile. It does not unlock items for other players, does not require hosting multiplayer, does not inject into Castle Crashers, and does not write to game process memory.

Changes stay staged until **Apply Changes** is pressed, and an automatic backup is created before the save is written.

## Build from source

Run `BUILD_CRASHER_UNLOCKER_V1_2.cmd` on Windows with .NET Framework 4.x available.

Output:

`Crasher Unlocker V1.2.exe`

No packer or obfuscator is used.

## Antivirus false-positive note

Some antivirus products may classify game save editors as GameHack, Potentially Unsafe, Generic, ML, or Anomalous because they locate game data, decrypt/re-encrypt a save, replace local files, and create/restore backups. The source code is included so the program can be reviewed directly.
