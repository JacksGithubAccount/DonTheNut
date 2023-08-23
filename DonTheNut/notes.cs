using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class notes
    {
        /* 11/24/2020
         * started notes
         * -currently: get animation to attack to play out once on button press 
         * -done, mc disappears for a frame at the end of attack animation
         * -done, cut off last frame in code check for attack animation
         * -currently: get mc to hold still for the full duration of attack animation
         * -done, set up bool to check attack animation and turned off movement control during attack animation
         * -currently: add collisionn detection, finish animations, add run
         * -done running animation on button toggle with faster speed
         * 11/25/2020
         * -learned about quadtrees
         * -put a hamster ball around MC, need to put on around skellyjelly
         * -put hamster ball on skelly, able to collide, but stuck on collision. Also, running can break collision
         * 11/26/2020
         * -done, fixed both by getting move direction and pushing mc back 1 pixel according to direction.
         * -made skellyboi move up and down
         * -problem: holding attack while standing still repeats attack animation
         * -done, changed animation back to idle when it normally checked for attack end
         * 11/27/2020
         * -currently: add camera that follows mc
         * -somehow managed to implement a resolution independant camera. Still need to make it follow mc.
         * 11/28/2020
         * -put off camera for now
         * -currently: add health
         * -added stats, need to add collision on attack before finishing health
         * -added ball on sword swing, but is doesn't work
         * 11/29/2020
         * -Got the sword swing to make the skeleton play the hit animation, but it happens at the start of the swing
         * -solved collision on attack at the right time, had to move code that checked for frames to top level of update
         * -Upon dying, the skeleton doesn't go through the last 2 frames of death animation
         * -total frames wasn't being calculated, making a method that did this solved the issue
         * -skel body dissapears after death animation plays out
         * -Added check for frame - 1 which solved the issue\
         * -body has collision; 
         * 11/30/2020
         * turned off collision by adding a class that is solely for interactions between two bodies
         * -damage somehow stopped working; turns out I missed a minus sign in the damage calc
         * 12/2/2020
         * -added healthbar, but the bar disappears in one hit. also need to separate healthbar into two files: inner/outer
         * 12/3/2020
         * -made a second health bar, couldn't continue any further due to keyboard issues
         * 12/4/2020
         * -keyboard works now. added notes for 12/3
         * -drew both health bars, current problem is the health bar depletes in a single hit
         * -Fixed health bar disappearing, turns out everything was done with int, switching to float fixed issue
         * -current issue: attacking hits 12 times instead of once
         * -fixed by usingg a bool that checks on first hit andc resets after hit animation finishes
         * 12/5/2020
         * -added skeleton attack animation, but doesn't work well with the skeleton getting hit
         * -currently: mashing the attack button doesn't cause damage after the first hit
         * 12/6/2020
         * -fixed attack mashing with no damage by moving the bool for damage from the one being hit to the attacker
         * -fixed skeleton hit animation by adding for frames to the png and having attack anim only play if wait bool is off
         * -currently: add player health and skeleton attack damage/collision
         * -moved a bunch of variables and methods that both brains shared into the super class they both use
         * -added player health bar and skellyboi can now damage the player. need to add hit animation
         * 12/7/2020
         * -added hit animation to player and it stops control when you get hit
         * -added player death with animation
         * -updated keyboard direction to make rolling diagnal work
         * -added new method for changing animation for reusability
         * -implemented rolling, but there's an issue where the frame check isn't working
         * 12/8/2020
         * -fixed frame check issue, apparently less than wasn't working but checking for equals does
         * -added backstep, same as roll but only moves backwards
         * -added block
         * -added animations for block hit and walking, still need to implement
         * 12/9/2020
         * -added block hit and walking
         * -currently: holding block allows resumed blocking after performing another action        
         * 12/10/2020
         * -checked in dark souls, turns out blocking works that way there too, is a nonissue
         * -currently: add stamina bar
         * -added stamina bar: attack, blocking damage, and dodge drains stamina. Blocking reduces recovery rate. Can't perform action with no stamina
         * -haven't implemented block breaks
         * -holding block then attacking causes attack animation to repeat, a fix is setting new animation on block press, but causes sprite to disappear when rapidly turning though
         * -fixed, set block animation when animations are set to finish when blocking solved issue
         * 12/11/2020
         * -implemented block break and stamina on run
         * -starting on AI, first establish line of sight
         * -started implementing quad tree, still kinda need to figure this one out, maybe instead of rectangles, take boundingspheres and boxes
         * 12/12/2020
         * -blocking broke. Issue was an implementation with sprinting with the stamina system. Now fixed.
         * -skeleton hit animation broke when attacking. Fixed by turning off wait if attacking
         * -may have to put off quad trees for now
         * 12/13/2020
         * -added line of sight, made skelly chase player when in range and attack when even closer
         * -added skelly walking animation, but skelly turns invisible after attacking
         * -fixed with walk check bool, but now skelly doesn't return to idle if out of line of sight
         * 12/14/2020
         * -implemented a chase bool which fixed issue
         * -current: implement menu system
         * -added menu and arrow keys and button press for newgame and exit
         * -added options, but buttons are not yet
         * 12/15/2020
         * -implemented options, more will come later but for now there is only fullscreen
         * -current: add in game menu for inventory/equipment/etc
         * -added screens for inventory/equipment and menu, currently working on implementing buttons for menu
         * 12/16/2020
         * -added an esc button in menu to return to game after opening menu, ran into a problem where esc menu didn't draw the game
         * -fixed by adding a bool that called the menu from other classes as the screen transition bool doesn't work for outside classes
         * -esc from menu resumes game where we left off, but the stamina bar increases while in menu though since the menu doesn't pause game
         * 12/17/2020
         * -solved pause issue by wrapping the game update in an else for the menu update
         * -ran into another issue where you need to press esc twice to open the menu. Pressing once only pauses the game. also the skelly takes 10 damage on start
         * 12/18/2020
         * -solved menu issue by using a menu call method. Now the menu opens and closes on button press first time, but opens properly on subsequent presses
         * -solved by bool check on esc button during menu open and using the keycheck for on button release instead of on button press
         * -issue where the skeleton moves too close to the player causing the player to go into the collision of skeleton
         * -solved by adding another bounding sphere this time to check for when to stop chasing
         * -added buttons for settings in menu and made a pop up window to return to title on pressing menu exit
         * -have not added buttons for the pop up window yet
         * 12/19/2020
         * -added buttons for the return to title screen pop up window
         * -added arrow keys for inventory, haven't added other button presses yet, probably wait until implement items to do so
         * 12/20/2020
         * -May as well implement items.
         * -Added classes for items and inventory for player
         * 12/21/2020
         * -made the inventory screen print out the player's inventory
         * -added int to make the inventory print the items in the first slot without gaps
         * -added arrow pressed to inventory items
         * 12/22/2020
         * -there's an issue with the pop up arrows diplaying about 187 pixels above where they should be showing.
         * 12/23/2020
         * -fixed issue, turns out the pixels were already calculated into the offset variable
         * 12/24/2020
         * -implement using items, created enum specifically for item effects
         * -ran into problem, how to select item from the inventory screen, can't go by slot since the inventory is split up by item type
         * -added a new list that only adds items printed to inventory and was able to use this list to select items
         * -limited arrow keys in inventory to only items showing in the inventory instead of all slots in inventory
         * -added using potions from iventory screen, which plays animation and heals near the end of the animation
         * -found another issue, the skeleton plays idle animation while chasing if you hit then move out of attack range
         * 12/25/2020
         * -fixed skeleton idle animation while chasing  by moving call for walk animation outside the walk check
         * -made item disappear from inventory when amount is 0 or less
         * -currently: add item drop and discard
         * -added discard from inventory menu, now need to add the item to the ground ingame after dropping
         * -added item image on ground, also can drop multiple items, now to add pickup item
         * 12/26/2020
         * -dropping multiples of the same item only shifts the location rather than have two copies of that item on the ground
         * -solved issue, turns out adding to list adds it as a ref, needed to create a new item for all items added to dropped item list
         * -added item pickup, but dropping another item after pickup causes nothing to drop
         * -figured issue out, I turned on action check on action button with nothing to turn it off, added turn off to top of update
         * -currently: add a messagebox that tells you what you picked up
         * 12/27/2020
         * -added messagebox, with it disappearing after a short amount of time and it displaying what you have picked up
         * -currently: 
         * 12/28/2020
         * -added esc button to cancel out of the popup menus in inventory
         * -currently: add button remapping
         * -added a couple new classes for input managing. Able to separate the Up key into an enum for moving up.
         * -need to do rest of the actions and also implement a way for the player to remap controls
         * 12/29/2020
         * -redone settings screen and added full screen options for windows, fullscreen, and borderless
         * -currently borderless doesn't stretch the screen, only expands it so there is a bunch of blank space
         * -started the remap screen, made the positions for the screen, need to finish rest
         * 12/30/2020
         * -drew screen, added the text for the actions and keyboard controls
         * -added method to reset remap to default, also up and down controls for remap
         * 12/31/2020
         * -added left/right controls and confirm allowing remapping of actions
         * -all key inputs are still hardcoded into the program, need to replace those with action checks also a default remap option
         * -theres also an issue with multiple inputs, the way input to action is set only allows one input at a time
         * 1/1/2021
         * -allowed multiple inputs by making actionkeys a list and adding button inputs to that and checking the list for any actions
         * -this however caused another issue where holding down multiple actions will cause both to accumulate and process even after the button is released
         * -solved by removing all instances of that action in the list as multiples of it are being added to the list
         * -also made it so the list only gets added to once per button press rather than continuously adding to it as doing that may cause issues down the line
         * 1/2/2021
         * -added input type to actionkey so I can determine if a key is pressed, held, or released
         * -started replacing the hardcoded key inputs in MC with the new inputter methods
         * -ran into issue where the press code keeps being recognized as pressed when you hold the button
         * -further testing shows that this is the case for press, hold, and release
         * 1/3/2021
         * -I think i figured it out, the control list is being altered itself when a type is added
         * -fixed by removing the reference to control, but now the check isn't working as it checks for that specific reference
         * -found .exists for lists able to check again, but controls are not working properly when there are multiple inputs
         * -the issue was that the removal hasn't been updated to use exist. After implementing, controls work normally as it were back when input was hardcoded
         * -ran into another issue, release sometimes works after multiple inputs of the same button
         * -after testing, release only works when inputted at the same time as another button
         * -found solution, in the update method there was a call to clear the list when there was no buttons pressed, removing this fixed the release input issue
         * -currently: add back button to settings menu, also remapping confirm is causing issues
         * 1/4/2021
         * -implemented back in remap, but going back causes all the buttons to not work
         * -solved, there a bool i forgot to turn off when entering remap screen
         * -added back in settings so it works on all the implemented screens in settings so far
         * 1/5/2021
         * -added a bool that turns on when confirm remap and have it turn off on specifically when confirm is released
         * -this solves the issue, but the pop up does repeat on remapping confirm to a different button
         * -removing the code that turns the bool on out of the if statement that checks for confirm fixed this issue
         * -next problem, you can remap buttons to another button with a different action
         * -implemented method in inputter to check if button has already been mapped to another action
         * -found another issue, after remapping any buttons besides confirm, need to press confirm again in order to remap any further
         * 1/6/2021
         * -fixed by adding method that checks for if any button has input type
         * -added a thing where you cannot remap button to a button that already has an action and displayed a message stating so and which action
         * -currently: add equipment
         * -added the slot bools and string var for equipment
         * 1/7/2021
         * -added gear slot image and text to the equipment screen, now to add keyboard input
         * 1/8/2021
         * -added arrow keys and cancel to equipment screen, also added confirm outside of popup, now to implement equipment on characters
         * 1/9/2021
         * -made some classes for equipments
         * 1/10/2021
         * -yeeeeeeees
         * 1/11/2021
         * -working on update method to add gear stats to character stats
         * 1/12/2021
         * -finished update method for gear stats to character stats
         * -reworked equipment screen to show these stats, also added gear images to equipment screen
         * 1/13/2021
         * -added new screen for gear selection after choosing to equip for a slot
         * -found issue where main hand magic atk was using phys atk for it's calculations, fixed issue by using bool to determine whether phys or not
         * -found an issue where inventory wasn't detecting the correct item on usage after a certain number. 
         * -fixed by changing an int that made it so the display inv wasn't working properly
         * -displayed items in gear selection depending on gear type to equip, need to finish arrows
         * 1/14/2021
         * -added arrow keys to gear selection, running into issue where the arrow keys aren't functioning properly
         * -fixed, had to  change ++ and -- to +=1 and -=1, not sure why this fixed it, but it did
         * -added equipping item from equipment screen, also added name display to gear selection
         * -found issue, equipping an already equipped item increase weight without removing it
         * 1/15/2021
         * -fixed weight issue by resetting the weight int whenever gear update is called
         * -added removing equipment
         * -found issue where you can equip the ssame weapon on multiple slots
         * 1/16/2021
         * -added part where if you equip an already equipped item, it removes the item from that slot and equips it to the selected slot
         * -ran into issue where you can equip nothing which crashed the game, added an if check which fixed it
         * -made it so after successfully equipping an item closes the gear selection screen
         * 1/17/2021
         * -learned about gui tools, might want to try making one later
         * -next, work on details for items, added screen for item details and positions
         * 1/18/2021
         * -added strings for details and was able to make the details display item details, need second screen to show description of item
         * -next add item equip, ran into issue where itemslot is matching with an uninstantiated object
         * -found issue was with bodypart conversion as I hadn't accounted for item equips yet. Adjusted for item equips.
         * -running into alot of issues with remove equipment method, i should be able to solve by dividing between equips and items as the issue is with bodypart
         * 1/19/2021
         * -added split to remove equipment
         * -found new issue where removing item1 and going to equip anything there shows gloves instead of consumables
         * -found cause, removing equipment doesn't account for items, fixed removing equips so it does account for items. Issue still persists.
         * -further tests shows that after selecting remove, the game thinks it's selecting remove even though it switches to equip. Fixing this fixed the issue
         * -found issue with equipping items, if an item is already equipped and a previous slot is empty, equipping that item will throw a null error
         * -fixed issue, had to go up a level on the item check to check the item slot
         * -the hat issue showed up again after closing the equipment screen and re-entering it. Resetting the int trackers upon equipment screen entry fixed this
         * 1/20/2021
         * -next, add arrows to equipment. Added arrows to equipment
         * -added slot selection to determine which slots are the active equipment slots for the player
         * -implemented item usage in main game, however the item doesn't disappear from equipment even though it's not in inventory anymore
         * -unequipped item from equipment slot when it is removed from inventory which solved the issue
         * -next, add item and main/offhand switching. Implemented switching for those three
         * -next, add UI for the main game
         * -added item tool bar and image for selected item
         * -next, add additional images for different items to test item tool bar
         * 1/21/2021
         * -added rock punch and images. Ran into issue where there is a null issue in draw for tool bar. Adding null check fixed this.
         * -found issue, equipping to the selected tool bar doesn't update the selected tool bar with the new item
         * -solved, added checks to equipping and removing items to/from tool bar
         * -next, implement rock punch as a usable item
         * -updated attack animation offset so that it applies to all animations where the frame dimensions are larger than the idle frames
         * -made Rock Punch appear on item use, but it follows player instead of using his initial position as a starting point
         * -used a global variable to store position only when player throws item, this fixed the issue
         * -the trajectory needs work, at the moment it only flies up
         * -also using rock punch again doesn't cause another one to show up
         * -fixed trajectory, it now  goes in an arc
         * 1/22/2021
         * -Added items to a list and made them all into new values by breaking ref then removing from that list and adding to a thrown list so they can be thrown individually.
         * -next, add collision for the thrown items
         * -first need to add left throws as the rocks only go right
         * -ran into issue where isleft was linked to player when throwing items. using samesolution for the position issue solved this
         * -added left throws, but the distance is much shorter than it's counterpart. Fixed issue I set to equal instal of times equal which solved issue
         * -ran into another issue where throwing right then throwing left causes the one thrown right to teleport where it would be if thrown left
         * -fixed issue, turns out the method or updating position was also updating every thrown item with the direction when a new item was thrown. Adding a new check and method fixed this
         * 1/23/2021
         * -went back to quadtrees and attempted to implement. I understand more of it now, but still unsure of how to use.
         * -added collision for rock punch and damage and also made rock punch disappear after hitting once
         * -currently: work on implementing a collision detection system that accounts for more than three hardcoded objects
         * -1/24/2021
         * -made lists to contain players, npcs, and throwable items. Ran loops that checked each against the other. This doesn't check objects in the same lists though
         * 1/25/2021
         * -added thrown item removal when thrown item exceeds boundaries of the screen. This is temporary until I can establish a map
         * -Trying to add two skellies now. Added two skellies. Also redone skelly into lists so adding an extra is made easier
         * -Ran into issue where hitting a skelly with rock punch isn't making them play the got hit animation
         * -testing shows that the issue is that the check gets turned off before any animation can play
         * 1/26/2021
         * -moving the bool flip from interaction to skelbrain fixed the issue. However, there is a new issue where the skeleton disappears after being hit twice
         * -testing shows that the issue is with the frame count as it doesn't reset after the skeleton finishes playing the hit animation
         * -turns out alot of the issues was a misplaced check on the finished animation check. removing that solved almost all of issues with animation that i ran into
         * -there is still the animation issue where leaving line of sight while the skeleton is walking causes them to walk in place
         * 1/27/2021
         * -fixed the skeleton idle issue. Had to add a check to the idle reset frame
         * -next issue is fixing the skeleton attack animation. The skeleton teleports when attacking
         * -fixed issue, problem was that I didn't update the draw method when I redid the animation to account for frame size
         * -implemented a second foreach loop in collision detection so skeletons can collide with each other.
         * -this caused an issue where the skeletons aren't moving even though they're walking.
         * -Testing shows that their move speed has been set to zero. Setting their move speed when they are called to walk fixes this issue.
         * -issue found where the skeletons can move into each other if they collide for long enough, will need to work on the collision code to fix this
         * -made collision more complex, you can still phase through the skeletons if they sandwich you, but it doesn't happen as often. 
         * -Also, if you walk towards into the skeleton, they will push you
         * 1/28/2021
         * -set move speed to 0 for both colliders solved the pushing issue
         * -another issue is that the skeletons are moving twice as fast
         * -fixed issue by moving the foreach loop for one of the skelly checks from outmost to just the part where it is needed
         * 1/29/2021         
         * -there is an issue where the skeletons can get stuck if they are touching and chasing the player
         * -added factions for characters to try and detect when they are colliding with teammates
         * 1/30/2021
         * -added more to collision, now the skeletons vibrate slightly in your direction when right of them
         * -being left of the skeletons make them vibrate slightly away from you
         * 2/1/2021
         * -split if check for distance between x and y, this now makes the skeletons vibrate in place instead of back when left of them
         * 2/2/2021
         * -added bools and floats to check or destination to add some kind of pathing for getting stuck on each other
         * 2/3/2021
         * -the skeleton currently moves out of the way a bit, but never resumes moving. The call to move is only once instead of consistently
         * 2/4/2021
         * -added the move call to update method for skelly and now the back skelly will move slightly up when colliding with a buddy
         * -tested with player on left, it doesn't work as the back skelly becomes front skelly and that one is still the one that moves
         * 2/5/2021
         * 2/6/2021
         * 2/7/2021
         * -since i'm getting stuck with collision, lets move on to something else for now. NPC behaviors
         * -added behavior for running
         * 2/8/2021
         * -added begavior for blocking, haven't implemented a way for them to switch behaviors after getting one though
         * 2/9/2021
         * -the movespeed is the same for all three behaviors
         * -added method that sets movespeed to the correct movespeed based on behavior checks
         * 2/20/2021
         * -
         * 3/5/2023
         * -finally getting back into stride
         * 3/6/2023
         * -attack animation was fucked showng the entire spritesheet
         * -Fixed by swapping the numbers for rows and columns when setting attack animation
         * -now guy's sprite is at a different spot when attacking, need to set an anchor point
         * 3/11/2023
         * 8/22/2023
         * -github attempt
         */
    }
}
