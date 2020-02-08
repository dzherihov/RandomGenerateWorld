# Dungeon Procedural Level Generator
![preview](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/example.gif)

## Introduction 
A universal algorithm for any game where procedural level generation is required. The generator can easily create up to 100 rooms without time delays.  

#### Asset features: 
* fast and thought-out algorithm for generating dungeons based on a binary matrix;
* conveniently create your own rooms using tile mappings
* ability to customize dungeon branching levels; 
* each generation has a unique dungeon. 

#### Asset included: 
* One demo scene; 
* 16 prefabs of various rooms; 
* One demo tilemap.

## Example Scenes 
An example scene demonstrates the operation of the algorithm. In order to see the algorithm in action, you need to run the scene. A dungeon will be generated on your screen.  
For new generation press «R». 

## Variety of rooms 
All rooms are prefabs, in turn, each can be customized to taste:

![prefabs](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/2020-02-08_00-30-25.png)

The first digit in the name indicates the number of exits from the room, the second is the serial number. For example, a prefab named «3_2» means that the room contains three exits: 

![example_room](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/2020-02-08_00-33-19.png)

A prefab named "noRoom" is needed to fill the empty space (where there are no rooms) when generating the level. 

To edit rooms, you need to use the tilemap tool. But this is just an example, you can do it as you like: 

![edit_room](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/2020-02-08_00-38-14.png)

## Generator settings 
For the functioning of the dungeon generator, you need to connect two scripts to the scene: 
1. GenerationMap - this script, which generates a binary matrix, on the basis of which, our dungeon will be built;
2. ArrayRooms - this script is directly responsible for the visual display of the dungeon itself. Based on the generated matrix, it compares our rooms and places them on the stage 

In the inspector, you need to configure the «ArrayRoom» script, add our room prefabs to the Room array field. It is also necessary to indicate the link to the parent object in the «Rooms Parent» field, in which the generated rooms will be placed according to the hierarchy of the object. It is necessary that everything is grouped:

![settings_room](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/2020-02-08_00-55-47.png)

In the «GeneratorMap» script inspector, specify the maximum number of branches for generating rooms in the «MaxRoomInspector» field:

![settings_generator](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/2020-02-08_01-00-21.png)

For example, if you specify a value of 3, then three rooms can be generated from the center in every four directions.

## How the algorithm works

![algorithm](https://github.com/dzherihov/RandomGenerateWorld/blob/readme.md/Assets/DungeonProceduralLevelGenerator/Documentation/algorithm.png)

## Support 
If you are confused by something, you can write to **denis@proger.xyz** and we will do our best to contact you immediately. We hope you enjoy our asset and good luck in creating your games! 
