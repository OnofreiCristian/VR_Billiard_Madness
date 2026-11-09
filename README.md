# VR_Billiard_Madness
Local bars hate this one trick!


Lab 4 homework: https://docs.google.com/document/d/1piOahs_ytPLMnKy0gdVYdzSpYwYHAnP8vEROz2fq0Us/edit?usp=sharing

Lab 5 homework: https://docs.google.com/document/d/1uQzS9Wt30iJg7r-76bo3VyhrUe2ACDtxCOq9vsssJgU/edit?usp=sharing

Lab 7 progress - Modeled walls, floor, ceiling for constructing a play area in a modular way; Also modeled a functional, better billiard table with holes that's functional and can be used to play the game.

Implemented an inventory system and created a more fitting scene. To add an item, we create its prefab and add it to a new InventoryItem asset. All that's left to do is to add it to the item list in InventoryUI, either dynamically or through the inspector (for starting items). 

InventoryItem - stores the item's name, icon and prefab
InventoryManager - spawns the clicked item at the SpawnPoint
InventoryUI - creates buttons dynamically and connects them to the Manager
ItemButton - button template
