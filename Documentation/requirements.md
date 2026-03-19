1. Core Game Structure
- The game must contain two seperate chapters: Japan and China
- Each chapter must load as an independent scene in Unity
- The player must be able to select chapters from a main menu
- Progress through each chapter must follow a linear sequence of cooking steps
- The player must be able to save their progress in a chapter and come back to it at the point they saved at a later time.

2. Cooking Interaction System
- The player must be able to drag & drop ingredients or click utensils to use them.
- Ingredients must have correct vs incorrect choices.
- The game must provide feedback (visual or audio) when the player chooses a wrong ingredient.
- The player must perform basic actions like add ingredient, stir, knead, grind or whist, wrap or shape, heat/boil/steam depending on the chapter

3. Chapter-Specific Requirements

Japan - Tea Ceremony Chapter
- Utensils needed: Matcha Bowl, Bamboo Whisk, Bamboo Scoop/Teaspoon, Fine Mesh Sieve, Matcha Powder, Kettle
- Step 1: Heat Water - The player must click the kettle and then wait for the visual cue of it being done (visual cue: green outline and/or steam above kettle) (optionally show UI tip that temp is important: 75-85°C, otherwise matcha burns)
- Step 2: Add Matcha - Player must drag Scoop -> Matcha -> Sieve and then drag Sieve with Matcha -> Bowl (for each mini-step work with outlines?)
- Step 3: Add Water - Player must drag Kettle -> Bowl (visual cue: bowl sprite change from empty to filled with water and/or green outline)
- Step 4: Whisk - The player must drag Whisk -> Bowl and then should do W-motion (visual cue: first the water turns green, then if the W motion is done, foam appears on the top) (ui tip: wisk should not touch bottom of the bowl to avoid damaging the bistles)
- Step 5: Enjoy matcha before it loses temp

Source: https://oryoki.de/blog/matcha-tea-preperation

China - Char Siu Chapter
- Utensils needed: Bowl, Oven, (Wooden) Spoon (for stirring), Table Spoon, Tea Spoon, Plate, Knife, Roasting Pan
- Ingredients needed: Pork, Honey, Salt, Sesame Oil, Chinese Rice Wine, Garlic, Oyster Sauce, HoiSin Sauce, Spices (Fennel, Cinnamon, Star Anise, Szechuan Pepper),  OPTIONAL: Rice, Pak Choi
- Step 1: The player must mix the ingredients (except for the pork) in a bowl in the correctly measured amount
- Step 2: Marinate the meat in the sauce for 24 hours
- Step 3: Heat up oven to 200°C
- Step 4: Bake meat for 30 min, turn meat and coat with marinade every few minutes.
- Step 5: Cook rice and wash pak choi
- Step 6: Turn up oven temperature to 230°C for 10 mins
- Step 7: Take meat out of oven and cut it into slices
- Step 8: Put rice, pak choi and meat on the plate

- TODO: Simplify or reduce steps

Source: Personal recipe from a friend

4. Cultural Context System
- Each step must trigger a short text or audio explaining cultural meaning
- Each chapter must end with a completion screen optionally explaining the cultural role of the dish and must provide a real life recipe.

5. User Interface
- The game must provide a consistent UI across chapters (but can have differences in colour for example to match the "vibe")
- UI must show the current step, the ingredient list and a hint for the action
- A pause menu must allow returning to the main menu.

6. Audio and Feedback System
- Each chapter must have distinct ambient soundscapes
- Correct actions must trigger positive cues (sound or visual)
- Wrong actions must trigger a corrective cue

7. Technical
- All chapters must work with mouse and keyboard.
- The game must handle a save state (depth to be discussed, either between steps or only after finishing the whole chapter) in a json format.

8. System Architecture Requirements
StepController (Finite State Machine)
InteractionManager
SaveManager
UIManager
AudioManager
Data-Driven Recipe System: Recipe, RecipeStep, Ingredient
