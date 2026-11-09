
# Inumamotchi — Level Design: Four Difficulty Levels

## Overview

Four levels share one house, one dog and one rule set. They differ in only two things: how many tasks the day holds, and how deep the escalation ladder goes when the owner does not react.

**What a level is.** The game has no end state, so a level is a selectable day template, not a stage to clear. Each template fixes the task list, the escalation depth, and (from Easy upward) how long the night runs on a bad day. Night stops count as tasks, so the design stays inside your two levers.

**A bad day is not Hard.** A level is authored and chosen by the player; a bad day is emergent, set by the owner's mood when the day starts. If the two were the same thing, Easy would never show a night and Hard would be misery on a loop, which breaks the "no despair" boundary. So a bad day can happen on any level except Tutorial, and the level only sets how long that night is.

**What makes a day bad.** The owner's baseline mood is below a threshold at Day Initialisation, usually after a Drift day or 2-3 days of neglect. The threshold value is untested and needs playtesting. Every bad day also carries two low-state events, one Floor and one Crying, described in their own section.

**Reading order.** Shared systems come first (house map, sprites, the dog's thought bubble, task library, escalation, night phase, low-state events). Each level then lists its day in order, with the flow of every task it contains. Task counts run 4, 6, 8, 10 across the levels; the comparison table at the end has the rest.

## House map and hotspots

Every task in this document hangs on an object already visible in the current background art, so no new rooms are needed.  

<img width="1669" height="942" alt="Bright Noon Storybook House" src="https://github.com/user-attachments/assets/bba324e5-693d-4a12-a036-cb510bc19dd4" />    

| Zone | Objects in the art | Used by |
| --- | --- | --- |
| Bedroom (top left) | Patchwork bed, two nightstands (one with a lantern), bookshelf headboard, window, laundry basket | Wake, Bedtime |
| Bathroom (top centre alcove) | Toilet, sink with mirror, glass shower | Scenery only |
| Kitchen (top right) | Counter, sink, stove and oven, fridge, window, hanging lamp | Breakfast, Dinner, night Kitchen stop |
| Entry (right wall) | Door with window, hook with red leash, shoe rack, small bowl on the floor, red wall box | Walk, Guard, Fetch Post |
| Dining nook (centre right) | Table with two chairs | End point of Breakfast, Lunch Nudge, Dinner |
| Living area (left and centre) | Green sofa with orange throw, low table with remote, rug, orange pouf, TV on a media unit, tall shelf | Play, night Sofa stop |
| Desk (bottom right) | Desk, monitor, keyboard, books, lantern-style timer, chair, plant shelf | Lunch Nudge, Desk Break, night Desk stop |
| Sunbeam (bottom left floor) | Window light patches on the floor | Rest Period spot |

**One screen plus the walk view.** The house is one painted background, the Walk is the only second view, and everything else is a clickable item or a sprite on top of the background. 

## Sprites on the background

With one painted background, how well the overlays blend decides whether the house looks alive or pasted together.

- **Layers.** From the back: the painted background (static props), item sprites (anything the dog carries, moves or highlights), character sprites (dog and owner), light overlays (night tint, lamp glows, breakdown desaturation), and the UI.
- **Clean plates.** Any object that becomes a sprite must be painted out of the background, or it shows twice. The art needs a clean plate under each one (table below).
- **Same camera and light.** Sprites are drawn at the background's isometric angle, palette and light direction, with a soft contact shadow baked into each one.
- **Depth sorting.** Characters and carried items sort by foot position, so the dog walks behind the sofa and in front of the dining table.
- **Highlights.** Every clickable item has a resting state and a glow state. The glow replaces any floating icon, which keeps the scene clean.
- **Night by overlay.** A dark tint over the whole scene plus additive light pools on the lantern, the kitchen lamp, the TV and the monitor. This avoids a second painted night background. I expect it to save effort but have not seen it on your art, so test it early.

| Item | In the art now | Used by |
| --- | --- | --- |
| Red leash | Hanging on the hook by the door | Walk |
| Shoes | On the rack by the door | Walk, Rung 2 |
| Small bowl | On the floor by the door (assumed to be the dog's) | Breakfast, Dinner |
| Orange throw | Over the sofa arm | Sofa night stop |
| Slipper, toy, letter | Not in the art; add as sprites | Wake Rung 1, Play and Desk Break, Fetch Post |

**Space for the floor events.** The open floor in the centre, between the dining nook and the sofa, is the only area big enough for an adult to lie down. The Floor and Crying events use it, so check that a lying owner sprite fits there without touching the rug edge.

**Sprite states.** Owner: asleep, sitting up, walking, eating at the table, typing at the desk, standing in the kitchen, on the sofa, on the bed edge, lying on the floor, crying. Dog: idle, walk, sit, lie, jump, bark, paw, carry, play bow, stay.

## The dog's thought bubble, change in tasks list ui

Small thought bubble above the dog that shows pictures, not sentences. Could be a text window with a dog portrait at the bottom of the screen, depend on play testing.

**Why a bubble.**

- The concept is that the player is told what to do, never why. Pictures give the what; sentences drift into the why.
- It suits all audiences, including a child who cannot read yet, and it needs no translation.
- The bottom of the background holds the desk, the sunbeam and the plant shelf. A text window would cover play space and compete with the owner for attention.
- It carries the game's comedy. The bubble can show the dog's misreading of the owner instead of the truth.

**Risk.** A bubble sits on a busy scene, so it must stay small and high contrast, and it can cover the owner when the two overlap.

**What the bubble shows**

- Instinct clue: the picture of the next target (bowl, leash, bed, a paw on the owner). It appears when a prompt fires and fades on the click.
- Rung hint: when a bad-day miss starts a ladder, the bubble shows the next rung's picture (a sign, then a paw, then the dog lying down), so the ladder teaches itself.
- Misread: after the owner's reaction, the bubble can show what the dog thinks happened. A short pet after barking is drawn as a medal, though the owner meant stop.
- Night: the bubble is dimmer and shows only the owner and the Stay picture.
- Never the owner's mood. That stays in body language.

**Same at every level.** Bubble timing and content do not change with difficulty, since levels only change tasks and rungs. The Tutorial adds a longer first-use hold on each picture.

**Fallback.** If playtests show pictures are not enough, add an optional caption toggle.

## Task library

Ten tasks cover all four levels. Each has one base flow, written here once and reused by every level below. Energy values are placeholders on a pool of 100. Every timed press uses the same cue: a ring shrinks toward a target zone, and the player presses while the ring is inside it.

**Wake** · morning · bedroom · Energy 4

1. The prompt highlights the owner in bed.
2. The player clicks the owner; the world locks and a Jump button appears.
3. The player presses Jump while the shrinking ring is inside the target zone.
4. Hit: the owner sits up at once and pets the dog (+Trust), and the next prompt fires.
5. Miss on a good day: the owner rolls over and the prompt returns after a short delay. Miss on a bad day: the owner stays down and the escalation ladder starts.

**Breakfast** · morning · bedroom to kitchen · Energy 7

1. A scent trail leads from the owner toward the kitchen.
2. The player clicks the owner and holds Nudge until the owner gets up.
3. The owner walks to the kitchen and the dog's bowl glows.
4. The player clicks the bowl and the dog sits.
5. The owner serves the dog and eats. Dog Appetite refills and owner Appetite rises.
6. Partial: the owner eats very little, with no Trust gain.

**Walk** · morning · entry, then the walk view · Energy 18

1. The prompt highlights the red leash on its hook, or the door.
2. The player clicks it; the owner puts on shoes from the rack, more slowly when mood is low.
3. The camera moves from the isometric house to the walk view.
4. The dog follows a fixed route; chasing something pointless is an optional detour.
5. The camera returns to the house and Energy is deducted.
6. Partial: the owner cuts the route short.

**Lunch Nudge** · midday · desk to dining nook · Energy 5

1. The owner sits motionless at the monitor and the desk glows.
2. The player clicks the owner and holds Nudge.
3. The owner glances down, stands, and walks to the dining nook.
4. The owner eats and owner Appetite rises.
5. Partial: the owner eats at the desk without stopping work.

**Fetch Post** (Hard only) · midday · entry to owner · Energy 5

1. A sound plays at the door and the red wall box glows.
2. The player clicks the box and holds to pull out the letter.
3. The player drags the letter to the owner, wherever the owner is sitting.
4. Releasing it at the owner's feet makes the owner take it and open it.
5. Partial: the owner sets the letter aside unopened.

**Desk Break** (Hard only) · afternoon · desk · Energy 5

1. The monitor glow pulses and the owner is hunched over the desk.
2. The player clicks the owner and presses Paw while the shrinking ring is inside the target zone.
3. Hit: the owner stretches, stands, and steps away from the desk toward the sofa.
4. Miss on a good day: the owner glances down, turns back to the screen, and the prompt returns later. Miss on a bad day: the ladder starts.

**Guard** · afternoon · window or door · Energy 7

1. An unpredictable event: a sound plays and a marker appears at a window or the door.
2. The player clicks the marker and presses Bark while the shrinking ring is inside the target zone, before the sound ends.
3. Hit: the owner looks up, checks, and gives the dog a short pet that means stop and relax. The dog read a threat; the owner saw a pigeon.
4. Miss on a good day: the owner gives a short, absent-minded pat and the marker stays until the sound ends. Miss on a bad day: the owner does not look up and the ladder starts.

**Play** · afternoon · living area · Energy 12

1. The toy appears on the rug and glows.
2. The player clicks the toy and drags it to the owner.
3. Dropping it makes the owner throw it once.
4. The player presses Catch when the toy lands; a hit earns an extra reaction.
5. The owner pets or smiles.
6. Partial: a weak throw and a short game.

**Dinner** · evening · kitchen to dining nook · Energy 7

1. A scent trail leads from the owner toward the kitchen.
2. The player clicks the owner and holds Nudge.
3. The owner goes to the kitchen, then sits at the dining nook.
4. The player clicks the dog's bowl and the dog sits.
5. Both eat. How much the owner leaves on the plate is the day's clearest mood cue.
6. Partial: the owner picks at the food.

**Bedtime** · last task · bedroom · Energy 2

1. The bed glows and the bedside lantern turns on.
2. The player clicks the bed; the dog circles and settles on the rug beside it.
3. The owner switches off the lantern.
4. The day summary resolves silently, with no score. The player chooses the next day or exit.
5. Bedtime has no timing window and no escalation ladder. On a bad day it waits for the night stops (see the night section).

### If the player does nothing

A prompt the player ignores persists until the next slot begins, which happens when the player finishes any other task or ends a rest. Then the owner handles it alone, and worse. Nothing runs on the real clock, and Trust never drops.

| Task | If ignored |
| --- | --- |
| Wake | The owner wakes late, alone and slowly |
| Breakfast | The owner skips it and the dog eats alone |
| Walk | No walk; the owner stays in |
| Lunch Nudge | The owner works through lunch |
| Fetch Post | The letter stays at the door |
| Desk Break | The owner stays at the desk |
| Guard | The sound passes and the owner never looks up |
| Play | No game |
| Dinner | The owner skips dinner |
| Bedtime | The owner goes to bed late and alone, and the day ends |
| Floor or Crying event | The owner stays low longer, recovery is slower, and the next day starts lower |

Every ignored task feeds Drift: lower owner mood and Appetite the next day, with no change to Trust. A ladder cannot be failed into silence: once it starts, it always ends in a response.

## Escalation ladders

On a bad day, when the owner does not react, a task escalates through up to three rungs, and every ladder ends in a response. Rungs are the only place subtasks exist.

**The principle.** Each rung is a different dog behavior aimed at the same goal, never an unrelated chore. They move from a sign (noise or an object), to contact (paw, nudge), to presence (staying close). Rung 3 is always a held Stay: the owner responds on their own time.

**How many rungs fire.** The ladder starts only on a bad day, and only when the player misses the timed press. On a good day a miss just re-prompts. Tasks with no timed press (Breakfast, Walk, Lunch Nudge, Fetch Post, Dinner) start the ladder when the owner does not respond to the first action, so on a bad day they always climb at least one rung. A hit on a bad day still completes the task, with a muted owner reaction. Rungs needed equal the owner's mood band within the bad day: Low 1, Very low 2, Lowest 3. The level's escalation depth caps the result. At the capped rung the owner always responds, so no ladder can dead-end. These bands are placeholders.

**What a rung pays.** A rung that lands pays a small Trust gain, smaller than a clean Complete, so nobody gains by missing on purpose. The task completes with a slower owner reaction and no bonus beat. Each rung has its own owner animation, so a deeper ladder shows the player more of the owner. Each rung costs 1 Energy. Energy at zero never blocks a rung; it only slows the dog and removes the timing bonus.

**What has no ladder.** Bedtime, the night stops and the low-state events. The night is already the quiet version of the day, and stacking rungs on it would be the death spiral described below.



A hit on the base action ends the task as a Complete. On a good day a miss just re-prompts. On a bad day a miss, or no response to an untimed action, starts the ladder: the dog climbs one rung at a time until it has done the rungs the owner's mood requires, or reached the level's cap. The owner then responds.

| Task | Rung 1: a sign | Rung 2: contact | Rung 3: presence |
| --- | --- | --- | --- |
| Wake | Whine at the bedside and fetch a slipper | Tug the blanket, lick the hand | Climb up and lie against the owner's side |
| Breakfast | Bring the empty bowl | Paw the owner's leg and lead to the fridge | Sit at the fridge door and wait |
| Walk | Sit at the door and whine | Carry a shoe from the rack to the owner's feet | Lie across the doorway and wait |
| Lunch Nudge | Whine beside the desk chair | Paw on the owner's lap | Rest the chin on the owner's knee |
| Fetch Post | Drop the letter at the owner's feet | Push the letter into the owner's hand | Lay the letter in the lap and rest beside it |
| Desk Break | Bring the toy to the desk | Paw at the owner's knee | Lie on the owner's feet under the desk |
| Guard | Bark again at the same spot | Stand between the owner and the door | Sit pressed against the owner's leg |
| Play | Drop the toy in the owner's lap | Play bow in front of the owner | Lie beside the owner with the toy and wait |
| Dinner | Bring the empty bowl | Paw the owner's leg and lead to the dining nook | Sit by the table and wait |

**Worked example: Wake, Very low mood, Normal level.**

1. The player presses Jump and misses the window. The owner needs 2 rungs.
2. Rung 1: the dog whines and fetches a slipper. The owner does not stir.
3. Rung 2: the dog tugs the blanket. The owner sits up slowly, with no pet bonus but a small Trust gain, and the next prompt fires.

**Why escalation is a ladder, not extra chores.** If a bad day only added unrelated tasks, bad days would feed bad days. A ladder spends the same effort on the same goal, ends in a guaranteed response, and pays a small Trust gain, so it never reads as a penalty.

## The night phase on a bad day

On a bad day the day does not stop at dusk. The same loop keeps running into the night, with fewer and quieter stops, because the night is the hardest stretch of a low-mood day.

**Continuity, not interruption.** Nothing wakes the dog. The Bedtime prompt is simply withheld because the owner is still up. The lights dim in steps, the clock keeps going, and there is no cut, no new screen and no "night" banner.

**Same loop, lower pressure.** Night stops use the standard prompt, click, lock, action, resolve cycle. Each stop is a held Stay, so there is no timing window and nothing to miss. A stop costs 0 Energy; the dog's tiredness shows in posture and pace, not in the meter.

**The owner wanders a fixed route.** The route is dining nook, desk, kitchen, sofa, bed. Each level's night is the tail of the Hard route, so a longer night means the owner makes more stops before bed.

**Light and tone.** Warm pools of light come from the objects already in the art: the nightstand lantern, the kitchen hanging lamp, the TV and the monitor. Colour desaturation stays reserved for breakdown events, so the player can tell a quiet night from a crisis.

| Night stop | Owner is | Flow (every step after the click is a hold) |
| --- | --- | --- |
| Desk (Hard only) | At the monitor, which is the only bright thing in the room | 1. The monitor pulses. 2. Click the owner's chair. 3. The dog lies down under the desk and stays. 4. The owner's hand drops to the dog's head and the monitor goes off. |
| Kitchen (Normal, Hard) | Standing under the hanging lamp by the sink or the open fridge | 1. The lamp glows. 2. Click the owner. 3. The dog sits against the owner's leg and stays. 4. The owner drinks water or eats a small bite (owner Appetite rises a little) and the lamp goes off. |
| Sofa (Easy, Normal, Hard) | On the sofa with the TV on, not watching it | 1. The TV glow pulses. 2. Click the sofa. 3. The dog jumps up, sits close, and stays. 4. Optional second beat: the dog tugs the orange throw over the owner. 5. The owner leans in and the TV goes off. |
| Bedtime, bad-day version (all) | Sitting on the edge of the bed with the lantern on | 1. The lantern pulses. 2. Click the bed. 3. The dog presses against the owner's knee and stays. 4. The owner lies down and the dog settles on the rug. 5. The day summary resolves silently. |



On a bad day the owner walks the same route at every level, and only the starting point moves. The Tutorial has no night.

**Skipping is safe.** The player can ignore any stop and signal Bedtime at any time. A skipped stop costs next-day mood (it feeds Drift) and never costs Trust.

**Low-state events at night.** Floor and Crying events can replace any night stop. They are described in their own section below, and the concept doc's breakdown rules apply unchanged.

**Nothing at night can fail.** Every stop resolves positively. Trust, owner mood and speed of response are the only things that vary.

## Low-state events: Floor and Crying

On a bad day the owner has two low moments that can land in the day or the night: lying on the floor, and crying. Both appear at every level except the Tutorial, and neither can go wrong.

- **How often.** Every bad day carries one Floor event and one Crying event. Each is placed in a gap between tasks or in a night slot when the day is assembled, weighted toward the night. The weighting is untested.
- **At night.** An event replaces a night stop instead of adding one, so the night length per level stays as in the comparison table. If the night has no free slot (Easy has one), the event moves to the day.
- **In the day.** The event fires between tasks, like Guard, and waits for the current task to finish.
- **Rules.** 0 Energy, no ladder and no timing window. Every comfort action works, Trust sets only the speed of recovery, and doing nothing prolongs the event and makes the next day harder, as in the concept doc.

**Floor event** · day or night · open floor in the centre of the room · Energy 0

1. The bubble shows the floor picture and the owner glows. Ambient sound dims a little; colour stays normal.
2. The player clicks the owner. Three comfort choices appear: Lie beside, Nudge the hand, Fetch the throw.
3. Any choice starts the calming animation, and the player holds to stay.
4. High Trust: the owner rests a hand on the dog, sits up, and later moves to the sofa. Low Trust: the owner turns toward the dog and sits up slowly.
5. Trust rises a little for the attempt.

**Crying event** · day or night · wherever the owner is nearest: sofa, bed edge or desk chair · Energy 0

1. Ambient sound fades and colour desaturates. The owner is silent and unmoving.
2. The prompt reads Comfort the owner, with four choices: Nuzzle, Sit Close, Bark Softly, Fetch Blanket.
3. Any choice starts the calming animation.
4. High Trust: the owner calms quickly and hugs the dog. Low Trust: the owner calms slowly, then pushes the dog away gently or simply stops crying.
5. Trust rises a little for the attempt. A clumsy but real comfort plays the Bond Deepened animation and unlocks a new subtle interaction.

**How the two differ.** The Floor event is quiet and keeps normal colour. The Crying event desaturates the scene and mutes the sound, which is how the player tells a crisis from a low moment.

## Level 1: Tutorial

The Tutorial is a four-task day with no escalation and no bad day. Its job is to teach the loop, the meters and how to read the dog's body language.

| Setting | Value |
| --- | --- |
| Tasks | 4: Wake, Breakfast, Walk, Bedtime |
| Escalation depth | 0 |
| Bad day and night stops | None; the owner stays at Good mood all day |
| Base Energy use | 31 of 100 (placeholder) |
| Worst-case Energy use | 31, because there are no rungs |

**What it teaches, in order**

1. Prompts and clicking: a highlight, a scent trail, a leash glow and a bed glow each fire once.
2. The lock: a click locks the world, and finishing the task unlocks it.
3. The timing cue: Wake teaches it with a ring that shrinks toward a target zone, and the player presses Jump while the ring is inside the zone. The cue is identical on every level.
4. Miss and partial: a missed Wake press shows the owner rolling over, and the prompt returns. Nothing is lost.
5. The meters: each appears the first time it moves. Wake shows Trust, Breakfast shows Appetite, Walk shows Energy. This assumes the three visible meters are Energy, Appetite and Trust; the concept doc does not name them.
6. Body language: after Breakfast the owner's posture and pace are the cue to read, with no hint text.
7. Rest: offered once in the sunbeam. Skipping it changes nothing visible.

**The day in order**

| Step | Task | Where | What the Tutorial adds to the base flow |
| --- | --- | --- | --- |
| 1 | Wake | Bedroom | Only the owner is clickable. A ring shrinks toward the target zone and the player presses Jump while it is inside. On a hit the owner pets the dog and the Trust meter appears. |
| 2 | Breakfast | Bedroom to kitchen | A scent trail leads to the kitchen. One paw icon explains the held Nudge. The Appetite meter appears and the bowl glows. |
| 3 | Rest (optional) | Sunbeam | A visible timer runs and a click ends it early. No consequence, since events are off. |
| 4 | Walk | Entry, then the walk view | The leash glows and the owner puts on shoes. One pointless detour is offered and can be skipped. The Energy meter drops on return. |
| 5 | Bedtime | Bedroom | The bed glows, the dog settles, the lantern goes off, and the summary stays silent. The player picks Begin next day or Exit. |

**What is switched off.** Guard events, random events, escalation, bad days, breakdowns, Floor events and night stops.

**Day end.** Begin next day opens Easy by default. Whether the player can pick any level there is an open question at the end of this document.

## Level 2: Easy

Easy adds two tasks and a first taste of escalation, and it is the first level where a bad day can happen.

| Setting | Value |
| --- | --- |
| Tasks | 6: Wake, Breakfast, Walk, Play, Dinner, Bedtime |
| Escalation depth | 1, on Wake, Breakfast and Dinner only |
| Bad day | Possible |
| Night stops on a bad day | 1: Sofa, then the bad-day Bedtime |
| Base Energy use | 50 of 100 (placeholder) |
| Worst-case Energy use | 53 (50 plus 3 rungs at 1 each) |

**Why those three tasks get a rung.** Waking and eating are where the owner's state shows most clearly, so the first lesson in "the owner may not respond" lands there. Walk, Play and Bedtime stay clean.

**A day, step by step**

| Step | Slot | Task | Good day | Bad day |
| --- | --- | --- | --- | --- |
| 1 | Morning | Wake | Jump hits; the owner sits up and pets the dog | Jump misses; Rung 1 (whine, fetch a slipper); the owner sits up slowly, no pet |
| 2 | Morning | Breakfast | Nudge works; owner and dog eat | Rung 1 (bring the empty bowl); the owner eats little |
| 3 | Morning | Walk | Full route, shoes on quickly | Shoes go on slowly and the route is cut short; no rung |
| 4 | Afternoon | Play | Throw, catch, extra reaction on a hit | Weak throw and a short game |
| 5 | Evening | Dinner | The owner eats well | Rung 1 (bring the empty bowl); the owner picks at the food |
| 6 | Night | Sofa stop | None; the day ends at Bedtime | The owner is on the sofa with the TV on; the dog sits close and stays |
| 7 | Night | Bedtime | Standard flow | Bad-day version at the edge of the bed |

**Flows that are new here.** Play and Dinner run exactly as written in the task library. Dinner reuses the Breakfast flow but ends in the dining nook.

**Rungs stop at 1.** Even at the Lowest mood band, Easy fires only Rung 1, and the owner always responds there.

**Rest.** Rest stays optional between tasks, in the sunbeam. Skipping it makes adverse events, including breakdowns, a little likelier later in the day.

## Level 3: Normal

Normal is the full intended day: eight tasks, up to two rungs on every task except Bedtime, and a two-stop night on a bad day.

| Setting | Value |
| --- | --- |
| Tasks | 8: Wake, Breakfast, Walk, Lunch Nudge, Guard, Play, Dinner, Bedtime |
| Escalation depth | 2, on every task except Bedtime |
| Bad day | Possible |
| Night stops on a bad day | 2: Kitchen, Sofa, then the bad-day Bedtime |
| Base Energy use | 62 of 100 (placeholder) |
| Worst-case Energy use | 76 (62 plus 7 tasks at 2 rungs of 1 each) |

**A day, step by step**

| Step | Slot | Task | Good day | Bad day |
| --- | --- | --- | --- | --- |
| 1 | Morning | Wake | Jump hits; the owner sits up and pets the dog | Jump misses; Rung 1 (whine, fetch a slipper), Rung 2 (tug the blanket, lick the hand); the owner sits up slowly |
| 2 | Morning | Breakfast | Nudge works; owner and dog eat | Rung 1 (bring the bowl), Rung 2 (paw the leg, lead to the fridge); the owner eats little |
| 3 | Morning | Walk | Full route, shoes on quickly | Rung 1 (sit at the door and whine), Rung 2 (carry a shoe over); shoes go on slowly and the route is cut short |
| 4 | Midday | Lunch Nudge | The owner leaves the desk and eats in the dining nook | Rung 1 (whine at the chair), Rung 2 (paw on the lap); the owner eats at the desk |
| 5 | Afternoon | Guard | Bark hits; the owner looks up and gives a short pet | Bark misses; Rung 1 (bark again), Rung 2 (stand between owner and door); the owner gives a slow, short pet |
| 6 | Afternoon | Play | Throw, catch, extra reaction on a hit | Rung 1 (toy in the lap), Rung 2 (play bow); a short game |
| 7 | Evening | Dinner | The owner eats well | Rung 1 (bring the bowl), Rung 2 (lead to the dining nook); the owner picks at the food |
| 8 | Night | Kitchen stop | None; the day ends at Bedtime | The owner stands under the lamp; the dog sits against the owner's leg; water or a small bite |
| 9 | Night | Sofa stop | None | The TV is on; the dog sits close and stays; optional throw tug |
| 10 | Night | Bedtime | Standard flow | Bad-day version at the edge of the bed |

**Guard floats.** Guard is an unpredictable event, so it can fire at any point after the Walk. The table shows where it usually lands. If it fires mid-task, it waits, because the world is locked during a task.

**Where the owner is at midday.** After the Walk the owner settles at the desk, which is what makes Lunch Nudge a nudge away from the monitor.

**What changes from Easy.** Four more tasks carry a ladder (Walk, Lunch Nudge, Guard, Play), every ladder is one rung deeper, and the bad-day night gains the Kitchen stop.

## Level 4: Hard

Hard is ten tasks, up to three rungs on every task except Bedtime, and a three-stop night on a bad day. It is the longest and slowest day, never the cruelest.

| Setting | Value |
| --- | --- |
| Tasks | 10: Wake, Breakfast, Walk, Fetch Post, Lunch Nudge, Desk Break, Guard, Play, Dinner, Bedtime |
| Escalation depth | 3, on every task except Bedtime |
| Bad day | Possible |
| Night stops on a bad day | 3: Desk, Kitchen, Sofa, then the bad-day Bedtime |
| Base Energy use | 72 of 100 (placeholder) |
| Worst-case Energy use | 99 (72 plus 9 tasks at 3 rungs of 1 each) |

**A day, step by step**

| Step | Slot | Task | Good day | Bad day |
| --- | --- | --- | --- | --- |
| 1 | Morning | Wake | Jump hits; the owner sits up and pets the dog | Rungs 1-3 (whine and slipper, tug and lick, climb up and lie against the owner); the owner sits up slowly |
| 2 | Morning | Breakfast | Nudge works; owner and dog eat | Rungs 1-3 (bowl, lead to the fridge, sit at the fridge and wait); the owner eats little |
| 3 | Morning | Walk | Full route, shoes on quickly | Rungs 1-3 (sit at the door, carry a shoe, lie across the doorway); slow shoes and a short route |
| 4 | Midday | Fetch Post | The letter is pulled, dragged and dropped; the owner opens it | Rungs 1-3 (drop it at the feet, push it into the hand, lay it in the lap and rest beside it); the letter is set aside unopened |
| 5 | Midday | Lunch Nudge | The owner leaves the desk and eats in the dining nook | Rungs 1-3 (whine at the chair, paw on the lap, chin on the knee); the owner eats at the desk |
| 6 | Afternoon | Desk Break | Paw hits; the owner stretches and steps toward the sofa | Rungs 1-3 (bring the toy, paw the knee, lie on the owner's feet); the owner finally leans back from the screen |
| 7 | Afternoon | Guard | Bark hits; the owner looks up and gives a short pet | Rungs 1-3 (bark again, stand between owner and door, sit pressed against the leg); the owner gives a slow, short pet |
| 8 | Afternoon | Play | Throw, catch, extra reaction on a hit | Rungs 1-3 (toy in the lap, play bow, lie beside the owner with the toy); a short game |
| 9 | Evening | Dinner | The owner eats well | Rungs 1-3 (bowl, lead to the dining nook, sit by the table and wait); the owner picks at the food |
| 10 | Night | Desk stop | None; the day ends at Bedtime | The owner is at the monitor; the dog lies under the desk and the monitor goes off |
| 11 | Night | Kitchen stop | None | The owner stands under the lamp; the dog sits against the leg; water or a small bite |
| 12 | Night | Sofa stop | None | The TV is on; the dog sits close and stays; optional throw tug |
| 13 | Night | Bedtime | Standard flow | Bad-day version at the edge of the bed |

**Flows that are new here.** Fetch Post and Desk Break are written in the task library. After lunch the owner returns to the desk, which sets up Desk Break, and the letter sound at the door can arrive while the owner is at the desk or on the sofa.

**Why the margin is thin.** A worst-case day uses 99 of 100 Energy, leaving one spare unit. If playtests show the worst case running over, cut the rung cost to 0.5 or trim base costs. Energy at zero never blocks a rung, so overrunning only slows the dog.

**Why Hard is not harsher.** Difficulty here is more of the day and deeper ladders. Every rung still ends in a response, the night stays quiet, and nothing is lost. Rung 3 is the emotional centre of the level: the dog does nothing except stay.

## The four levels side by side

Only task count and ladder depth grow from level to level; the length of the bad-day night follows from the task count.

| Level | Tasks | Added tasks | Escalation depth | Tasks with a ladder | Night stops on a bad day | Base Energy (of 100) | Worst-case Energy |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 1 Tutorial | 4 | None | 0 | 0 | None (no bad day) | 31 | 31 |
| 2 Easy | 6 | Play, Dinner | 1 | 3 | 1 | 50 | 53 |
| 3 Normal | 8 | Lunch Nudge, Guard | 2 | 7 | 2 | 62 | 76 |
| 4 Hard | 10 | Fetch Post, Desk Break | 3 | 9 | 3 | 72 | 99 |

Everything else is identical across levels: timing windows, the Energy pool, rung cost, mood bands, rest rules, Trust, the thought-bubble clues and the bad-day events. All Energy numbers are placeholders.

## Variant pool (backlog)

Each task slot can later rotate between variants, so the player does not memorise one flow. The base flow in the task library ships first; everything here is for later.

- **Cost-neutral.** A variant has the same Energy, the same ladder shape (sign, contact, presence) and no extra timing precision, so variants never change difficulty.
- **Rotation.** Each day picks one variant per slot and avoids repeating yesterday's. The Tutorial always uses the base flow.
- **Cheapest first.** Variants that reuse objects already in the art need the least new work.

| Slot | Base | Variant B | Variant C |
| --- | --- | --- | --- |
| Wake | Jump on the bed | Tug-of-war: hold to pull the blanket, release at the peak | Soft howl: hold to build the sound, release at the peak |
| Breakfast | Nudge the owner to the kitchen | Owner already in the kitchen: bring the bowl from the door | Sit and stare at the fridge until the owner opens it |
| Walk | Leash from the hook | Missing shoe: follow a scent to a shoe under the sofa and carry it over | Rainy day: the owner takes a coat and the route is shorter |
| Lunch Nudge | Nudge from the desk | Owner on a call: stay still until it ends | Bring the bowl to the desk |
| Guard | Noise at the window or door | Doorbell: lead the owner to the door | Wind rattles the window: press against the owner's leg |
| Play | Fetch the toy | Pouf push: nose the orange pouf across the rug | Hide and seek: find the toy under the sofa |
| Desk Break | Paw at the knee | Window watch: sit at the window until the owner joins | Sunbeam invite: lie in the sunbeam and wait |
| Fetch Post | Pull out the letter | Parcel: a larger item dragged more slowly | False alarm: the box is empty and checking is still rewarded |
| Dinner | Nudge to the kitchen | Takeaway: the doorbell rings and the dog guides the bag to the table | Dishes: sit beside the owner at the sink until they sit down |
| Bedtime | Click the bed | Bring a toy to the bed | Settle at the foot of the bed instead of the rug |

**Extra quiet tasks to add later**

- Fetch the remote from the low table.
- Share the sunbeam: the owner joins the dog on the floor light.
- Look at the pictures on the left wall: the dog sits under them and the owner looks up. A narrative beat.
- Laundry basket: nudge the basket by the bed so the owner tidies.
- Plant check: sit by a plant shelf until the owner waters it.
- Reading company: sit by the bookshelf until the owner picks a book.

## Open questions and tuning

**Decisions**

1. Level choice after Easy: by day number, by Trust, or by the player? The Tutorial to Easy handover is settled.
2. Tutorial night: Unsure about a gentle one. I lean toward keeping the Tutorial free of bad days
3. Bad-day hits: today a hit on a bad day completes the task with a muted reaction and skips the ladder. Should a hit shorten the ladder instead, so skill still matters on bad days?
4. Event frequency: one Floor and one Crying event on every bad day at every level, Easy included. Is that right for Easy, or should Easy get only one?
5. Night stops: no ladders and no timing windows. Is the held Stay is the right action.
7. Timing cue: a shrinking ring fits "no reflexes required" only if the target zone is generous. TBD the cue and how forgiving the zone should be.
8. Clue system: he picture-only thought bubble, or choose the text window with a dog portrait.

**Numbers to playtest**

- The mood bands and the bad-day threshold at Day Initialisation.
- Energy costs per task and the rung cost of 1. Hard's worst case sits at 99 of 100, so it has almost no slack.
- Trust sizes: a Complete versus a rung, and whether deeper rungs pay more.
- The ring: shrink time and target-zone width.
- How strongly the Floor and Crying events lean toward the night.
- Whether three rungs on nine tasks is tedious or satisfying. I do not know; only playtesting answers it.
- Whether a three-stop night feels long. Same caveat.
