# 25-prj02-kinderspiel-ptaitl

This game was created in the context of the MTIN lessons with Prof. Meerwald-Stadler, Dipl.-Ing. Susanne of the HTL Salzburg. (School year 2022/23)

> **Note:** The finished game can be played [Here!](https://5ahmns-wisem22-23.github.io/25-prj02-kinderspiel-ptaitl/)

## Assignment

There are three analog games to choose from. One of them is to be recreated in digital form with Unity.

## Game Logic

The following game logic results from the additional features:

A car has 60 seconds to pick up as many gifts as possible and bring them to the gift bag. When the time is up, the game is over and it is checked if a new high score has been achieved. Also, the player (the car) has some health. Whenever it passes over a kill zone, it loses health. If all lives are used up, the game is over and it is checked whether a new highscore has been achieved.

![Spielbeschreibung](https://user-images.githubusercontent.com/72389299/215900654-5ac38d94-84a2-46c6-870b-40518ed71b24.png)


## Additional features

Besides the required tasks, I have added the following features:

- Camera Smooth Damping
- Car Reverse Rotation Adjustment
- SpeedUp, SlowDown & Death Trap Zones (Zones Controller with Custom Inspector)
- Timer
- Healthbar
- Highscore System
- Out of bounce effect
- Music (incl. audiolistener logic across scenes)
- Snow Particle System
- Minimap

## User Testing

The game was tested by three classmates who each wrote issues. No bugs were found.

## Packages

| Package Name              | Package ID                 | Version |
| :------------------------ | :------------------------- | :------ |
| 2D Animation              | com.unity.2d.animation     | 5.0.7   |
| 2D Pixel Perfect          | com.unity.2d.pixel-perfect | 4.0.1   |
| 2D PSD Importer           | com.unity.2d.psdimporter   | 4.1.0   |
| 2D SpriteShape            | com.unity.2d.spriteshape   | 5.1.4   |
| JetBrains Rider Editor    | com.unity.ide.rider        | 2.0.7   |
| Post Processing           | com.unity.postprocessing   | 3.1.1   |
| Test Framework            | com.unity.test-framework   | 1.1.29  |
| TextMeshPro               | com.unity.textmeshpro      | 3.0.6   |
| Timeline                  | com.unity.timeline         | 1.4.8   |
| Unity UI                  | com.unity.ugui             | 1.0.0   |
| Version Control           | com.unity.collab-proxy     | 1.9.0   |
| Visual Studio Code Editor | com.unity.ide.vscode       | 1.2.3   |
| Visual Studio Editor      | com.unity.ide.visualstudio | 2.0.11  |

> **Note:** All packages used are permanently free and included in the repository. No additional packages need to be installed when cloning the project.

## Assets

All sprites are either copyright free or have been modified to the point where there should be no legal issues.

## Project configuration

- PC, Mac & Linux Standalone
- Unity Editor 2020.3.18f1
- Visual Studio 17.4.1
- macOS Ventura 13.0 auf MacBook Pro 15", 2018

## Final note

Since this is a school project, all scripts are commented in great detail to make it easier to judge the work and to understand the context. So these comments are not to be judged as code smells.

Copyright © 2023 by ptaitl
