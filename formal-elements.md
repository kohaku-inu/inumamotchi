# inumamotchi 犬守っち


*Every good boy knows how to take care of his human!*

---

## 1. Description

**Inumamotchi** (inu "dog" + mamo, short for *mamoru*, "to guard" — with a wink at the inspiration) is a 2D isometric, single-player game in which the player takes the role of a dog caring for its owner. The perspective inversion is the whole point: the player is the pet, the human is the one being looked after.

The game is inspired by the classic **Tamagotchi** (*tamago* "egg" + *uotchi* "watch"), released by Bandai in 1996. Where Tamagotchi asks you to keep a creature alive, Inumamotchi asks you to keep a *person* functioning — filtered entirely through what a dog can perceive, understand, and misunderstand.

Play unfolds across a continuous day cycle of small domestic tasks. The dog's instincts guide the player, the owner's behaviour provides the cues, and the gap between the two produces both the comedy and the emotional weight. There is no villain and no ending: only days, and the slow accumulation of trust.

---

## 2. Core Concepts

1. **Single player, all audiences.** Designed for any age, any skill level, and any amount of gaming experience. No reflexes required, no prior genre literacy assumed.

2. **Instinct as instruction.** The player's tasks are framed as doggy instincts rather than quests. Objectives surface as on-screen prompts (a scent trail, a whine, a paw tap) and as unpredictable events marked on screen. The player is never told *why* — only *that*.

3. **Point-and-click with embedded mini-games.** All actions are performed by pointing and clicking, with the architecture deliberately left open so that specific tasks can later expand into short mini-games.

4. **No end state.** The game is a continuous day cycle. Each day concludes when its tasks and goals are complete, offering the player the choice to begin another day or exit. There is no finale, no credits roll, no completion percentage.

5. **Two resource economies.** The dog's resources regenerate reliably; the owner's do not, and depend entirely on the player.

6. **Implicit narrative.** Story is delivered in the background — through the owner's behaviour, the state of the house, and what the dog notices. Nothing is explained outright.

7. **Safety in vulnerability.** During emotional breakdown events, *any* action taken by the dog yields a positive outcome. The variable is the speed of recovery, not the success/failure of the interaction. The dog can never make the owner worse by trying to help.

---

## 3. Rules

**Interaction rules**
- One task per interaction; the world is locked while a task UI is active.
- Tasks are initiated by clicking a highlighted object, character, or prompt marker.
- Some tasks include a timing window; pressing *promote* within that window earns a reaction from the owner and adds to the relationship.
- Certain tasks can be resolved as mini-games; the surrounding flow is identical either way.

**Resource rules**
- Energy and Appetite limit how much the dog can accomplish in a day.
- Both drain through activity and regenerate overnight.
- The dog's resources recover predictably. The owner's do not.
- Trust is persistent, rises slowly, and **never resets**.

**Rest rules**
- Rest periods are available between tasks.
- Skipping a rest period raises the probability of certain events occurring later in the day.
- Resting is never mandatory, and never punished directly — it only shifts probabilities.

**State rules**
- The day cycle is not the only continuously changing state. Owner mood, house atmosphere, and event likelihood all shift beneath it.
- No rule can produce a permanent loss, a hard game-over, or an unwinnable state.

**Breakdown rules (Special Case)**
- When the owner enters a breakdown state (crying, shutdown), the standard "risk of wrong action" is suspended.
- All available comfort actions (nuzzle, sit, bark softly, fetch item) reduce the intensity of the breakdown.
- The *speed* of recovery depends on the current **Trust** level, not the specific action chosen.
- Doing nothing prolongs the breakdown and increases the difficulty of the following day.

---

## 4. Goals

**Per-task.** Complete the currently prompted instinct — wake, feed, walk, nudge, play, guard, settle.

**Per-day.** Fulfil the day's set of tasks and goals, then choose to continue to the next day or stop.

**Ongoing.** Raise Trust through repeated, attentive interaction. Trust carries across all days and is the game's true measure of progress.

**Player-facing reward.** The player is rewarded for *seeing more* — noticing the owner's state, catching subtle cues, and recognising the hidden labour of being a man's best friend.

---

## 5. Processes

These are the concrete sequences the game runs on.

### 5.1 Day Initialisation
1. Day counter increments and displays (Day 1, Day 2, …).
2. Dog's Energy and Appetite reset to full.
3. Owner's state is calculated from the previous day's outcome: baseline mood, appetite, and willingness to engage.
4. The day's task list is assembled, weighted by owner state and by whether rest periods were taken.
5. Scene loads at the bedroom with the dog beside the bed.

### 5.2 Instinct Prompt Cycle *(the core loop)*
1. An instinct fires — a prompt appears on screen (icon, scent trail, or subtle highlight on an interactable).
2. Player clicks the highlighted target.
3. The world locks; the task UI opens.
4. Player performs the task (single click, timed press, or mini-game).
5. Outcome resolves — success, partial, or missed timing.
6. Feedback plays: owner reaction animation, sound, and a Trust delta if applicable.
7. World unlocks; the dog returns to free movement.
8. Repeat until the day's task list is exhausted.

### 5.3 Wake the Owner *(worked example)*
1. Prompt highlights the owner in bed.
2. Player clicks the owner.
3. Task UI opens with a jump action.
4. Player presses to jump.
5. Owner stirs; if the press lands inside the timing window, the owner sits up immediately and pets the dog (+Trust).
6. If timing is missed, the owner rolls over and the task re-prompts after a short delay.
7. On success, the next instinct fires (breakfast).

### 5.4 Walk Sequence
1. Prompt highlights the leash or the door.
2. Player clicks it; a short preparation step plays (owner puts on shoes — slower if owner mood is low).
3. Camera transitions from the interior isometric view to the walk view.
4. Player moves along a fixed route; the dog may chase something pointless as an optional detour.
5. Walk duration and owner responsiveness scale with owner state.
6. Return transition restores the interior view; Energy cost is deducted.

### 5.5 Rest Period
1. Between tasks, the dog may lie down.
2. A rest timer runs; the player may end it early.
3. Resting reduces the probability of adverse events later in the day.
4. Skipping rest does not block anything — it only loads the dice.

### 5.6 Task Resolution & Feedback
- **Complete:** task UI closes, owner reacts positively, Trust increases.
- **Partial:** task completes but without bonus reaction; no Trust gain.
- **Missed timing:** task remains available; owner reaction is neutral or mildly dismissive.
- **Ignored:** the instinct prompt persists or expires; the day's task list lengthens slightly.

### 5.7 Breakdown Event *(Emotional Crisis)*
1. **Trigger:** Random low baseline OR cumulative neglect over 2–3 days.
2. **State:** Owner enters a shutdown state (crying, silent, unmoving). Ambient sound fades, colors desaturate.
3. **Prompt:** "Comfort the owner." Multiple action options appear (Nuzzle, Sit Close, Bark Softly, Fetch Blanket).
4. **Resolution:**
   - Player selects *any* action.
   - All actions trigger a "Calming" animation sequence.
   - **High Trust:** Owner calms quickly, hugs the dog, mood stabilizes.
   - **Low Trust:** Owner calms slowly, pushes away gently or just stops crying, mood improves slightly.
   - **No Action:** Breakdown continues, draining the day's remaining time and increasing next-day difficulty.
5. **Outcome:** Breakdown ends. No "wrong" choice exists. Trust may increase slightly for any attempt made.

### 5.8 Day End
1. All tasks resolved (completed or expired).
2. Bedtime instinct fires; player signals bedtime and the dog settles.
3. Day summary resolves silently — no score screen, no grade.
4. Owner state is written forward to the next day.
5. Player is offered two options: **begin the next day**, or **exit**.

---

## 6. Resources

Resources are split between the two characters. This asymmetry is the design's spine.

### The Dog's Resources — *regenerate reliably*
| Resource | Behaviour |
|---|---|
| **Energy** | Limits total actions per day. Drains with movement, play, and walks. Fully restored each night. |
| **Appetite** | Rises through the day; satisfied by breakfast and dinner. Affects willingness to perform tasks. |

Displayed as visible meters.

### The Owner's Resources — *do not regenerate on their own*
| Resource | Behaviour |
|---|---|
| **Mood** | Baseline state carried between days. Raised by attentive care, lowered by neglect or misread cues. |
| **Appetite** | Requires the dog to nudge. Will not recover unprompted. |
| **Attention** | Limited pool. Over-demanding it causes withdrawal; under-using it causes drift. |

### Shared
| Resource | Behaviour |
|---|---|
| **Bond / Trust** | Persistent memory of the relationship. Rises slowly, **never resets**, and carries across every day. |

**Presentation rule:** three of these are shown as visible meters. The remainder are communicated through the dog's body behaviour — posture, tail position, pace, hesitation — so the player reads the animal rather than the HUD.

---

## 7. Conflicts

**Primary — Player vs. Environment.** The dog wants to act; the house, the weather, the owner's inertia, and the dog's own limits resist. Doors are shut. Shoes are missing. The owner will not get up. This is the mechanical friction of every task.

**Secondary — Misunderstanding.** The comic and emotional engine. The dog interprets human behaviour through canine logic and gets it wrong in ways that are funny, then quietly sad. The owner's sigh might be exhaustion or indigestion. The closed door might be rejection or a nap. The player acts on the dog's reading, not the truth.

**Tertiary — Instinct vs. Obligation.** The dog wants to chase the squirrel. The dog is supposed to nudge the owner to eat. Both are legitimate drives; only one is on the task list.

**Quaternary — Emotional Crisis.** The conflict here is not "right vs. wrong" but "presence vs. absence." The challenge is to stay engaged during a breakdown, knowing that any effort helps, but that the recovery speed depends on the history of the bond.

---

## 8. Boundaries

**Spatial.** A single home interior plus a fixed neighbourhood walking route. No open world, no procedural generation. Visually, the house is composed of detailed hand-drawn background screens covering each distinct area — the intended look and density is comparable to *Shin-chan: Shiro and the Coal Town*.

**Temporal.** One in-game day maps to one play session. Days are sequential and cumulative but never timed against the real clock.

**Mechanical.** The dog cannot speak, cannot use tools, and cannot reason abstractly. All interaction is body language, proximity, sound, and physical presence.

**Tonal.** Warm, wry, observational. No cruelty, no punishment, no despair. Difficulty is expressed as slowness and quiet, never as hostility.

**Narrative.** The story stays implicit. Nothing is explained directly; the player assembles it from behaviour and environment.

**Emotional Safety.** During breakdown events, the game guarantees that no action taken by the player will worsen the owner's state. The only risk is inaction.

---

## 9. Outcomes

Because the game cannot be failed permanently, outcomes are graded by **how much the player noticed** — not by whether they won.

| Outcome | What it looks like |
|---|---|
| **Full Day** | All tasks completed with timing bonuses. Owner visibly brighter. Trust gains accumulate. Extra reactions and small narrative beats unlock. |
| **Quiet Day** | Tasks completed, timing mostly missed. Owner functional but flat. Trust holds steady. Nothing is lost. |
| **Drift** | Several tasks ignored or mistimed. The owner withdraws slightly — sleeps longer, eats less, engages less. The next day starts slower and requires more effort. **Nothing is forfeited.** |
| **Deep Day** | Rare. High attentiveness sustained across many days produces moments of genuine mutual understanding — the owner responds to the dog unprompted, and the dog's readings become more accurate. |
| **Breakthrough** | After a breakdown event, if the player successfully comforts the owner (even clumsily), a unique "Bond Deepened" animation plays, unlocking a new subtle interaction for future days. |

**On "failing":** Drift is the closest the game comes. It is not a loss state — it is the owner having a bad stretch, and the dog's job continuing tomorrow. Trust never decreases. No progress is erased. The player is never told they did badly; they simply notice the house is quieter, and the game trusts them to care.

**On Breakdowns:** These are not failures but turning points. They test the player's willingness to stay present. The outcome is always recovery, but the *quality* of recovery depends on the strength of the bond.

---

## 10. Games Survey

Searches on Google Play and the wider web for **"tamagotchi-like games"** return titles that almost universally keep the player in the caretaker-of-a-creature role. A separate search for **"dog taking care of owner game"** returns only variations on *taking care of a dog* — the inverse framing does not appear to exist as a shipped commercial product.

Existing comparators — *Pou*, *My Forever Tamagotchi*, *Nintendogs*, *Neopets* — differ from Inumamotchi principally by swapping which creature is being cared for, while retaining the same direction of care.

**Inumamotchi's distinction is the reversal itself:** the player is the animal, the dependent is the human, and the interface is the dog's perception rather than a management dashboard. Layered onto that is a witty tone, an implicit story running quietly in the background, and a unique safety mechanic where love is never the wrong answer.

<div dir="rtl">
 
# אינוממוטצ'י - אלמנטים רשמיים
*כל כלב טוב יודע איך לטפל בבן האדם שלו!*

---

## 1. תיאור

**אינוממוטוצ'י** (Inu = כלב + Mamo, קיצור של *mamoru* = "להגן" — עם רמיזה להשראה) הוא משחק לשחקן יחיד בתצורה דו-ממדית איזומטרית, בו השחקן מגלם כלב המטפל בבעלים שלו. ההיפוך הזה הוא הנקודה המרכזית: השחקן הוא חיית המחמד, והאדם הוא המטופל.

המשחק מושפע מה-**Tamagotchi** הקלאסי (*tamago* = ביצה + *uotchi* = שעון), שיצאה לאור על ידי Bandai ב-1996. בעוד שב-Tamagotchi המטרה היא לשמור על חיה בחיים, אינוממוטוצ'י מבקשת ממך לשמור על *אדם* מתפקד — דרך נקודת המבט של הכלב ומה שהוא יכול להבחין, להבין ולפרש בצורה שגויה.

המשחק מתרחש במחזור יום רציף של מטלות ביתיות קטנות. האינסטינקטים של הכלב מנחים את השחקן, התנהגות הבעלים מספקת את הרמזים, והפער בין השניים יוצר גם את הקומדיה וגם את המשקל הרגשי. אין יריב ואין "משחק נגמר": רק ימים, ובניית האמון.

---

## 2. מושגי ליבה

1. **שחקן יחיד, לקהל רחב.** מתאים לכל גיל, כל רמת מיומנות וכל ניסיון משחק. לא נדרשות תגובות מהירות, ולא מניחים ידע קודם בז'אנר.

2. **אינסטינקט כהוראה.** המשימות של השחקן מוצגות כאינסטינקטים של כלב במקום כמשימות (quests). האובייקטים מופיעים כהנחיות על המסך (שביל ריח, נביחה, נגיעה ברגל) ואירועים בלתי צפויים. השחקן לעולם לא מקבל הסבר *למה* — רק *שהוא צריך*.

3. **הצבע והקלק עם מיני-משחקים מוטמעים.** כל הפעולות מתבצעות על ידי הצבעה והקלקה, כאשר הארכיטקטורה נשארת גמישה כדי לאפשר הרחבה עתידית למיני-משחקים ספציפיים.

4. **אין מצב סיום.** המשחק הוא מחזור יום רציף. כל יום מסתיים כאשר המשימות והיעדים שלו הושלמו, ומציע לשחקן לבחור להתחיל יום חדש או לצאת. אין סוף, אין כתוביות, אין אחוזי השלמה.

5. **כלכלת משאבים כפולה.** המשאבים של הכלב מתחדשים באופן עצמאי; המשאבים של האדון לא, ותלויים לחלוטין בשחקן.

6. **נרטיב מרומז.** הסיפור מוצג ברקע — דרך התנהגות הבעלים, מצב הבית, ומה שהכלב מבחין בו. כלום לא מוסבר בגלוי.

7. **בטיחות בפגיעות.** במהלך אירועי התפרקות רגשית, *כל* פעולה שננקטת על ידי הכלב מניבה תוצאה חיובית. המשתנה הוא מהירות ההתאוששות, לא הצלחה/כישלון של האינטראקציה. הכלב לעולם לא יכול להחמיר את מצבו של הבעלים בניסיון לעזור.

---


## 3. כללים

**כללי אינטראקציה**
- משימה אחת לכל אינטראקציה; העולם ננעל בזמן ש-UI המשימה פעיל.
- משימות מופעלות על ידי לחיצה על אובייקט, דמות או סימן הנחיה מודגש.
- חלק מהמשימות כוללות חלון זמן; לחיצה על "פעולה" בתוך מסגרת זמן מביאה לתגובה מהבעלים ומוסיפה לקשר.
- משימות מסוימות יכולות להיפתר כמיני-משחקים;

**כללי משאבים**
- אנרגיה ותיאבון מגבילים כמה הכלב יכול להשיג ביום.
- שתיהן מתרוקנות דרך פעילות ומתחדשות בלילה.
- המשאבים של הכלב מתאוששים באופן צפוי. המשאבים של הבעלים לא.
- אמון הוא קבוע, עולה לאט, ולעולם **לא מתאפס**.

**כללי מנוחה**
- ישנם פרקי מנוחה בין משימות.
- הימנעות מפרק מנוחה מעלה את ההסתברות לאירועים מסוימים מאוחר יותר ביום.
- מנוחה לעולם אינה חובה, ולא מענישה ישירות — היא רק משנה הסתברויות.

**כללי מצב**
- מחזור היום אינו המצב היחיד שמשתנה ברציפות. מצב הרוח של הבעלים, האווירה בבית, והסתברות אירועים משתנים כתוצאה מכך.
- אף כלל לא יכול לייצר אובדן קבוע, סיום משחק קשה, או מצב שלא ניתן לנצח בו.

**כללי התפרקות (מקרה מיוחד)**
- כאשר הבעלים נכנס למצב התפרקות (בכי, כיבוי), ה"סיכון לפעולה שגויה" הרגיל מושעה.
- כל פעולות הנחמה הזמינות (ליטוף, ישיבה קרוב, נביחה רכה, הבאת פריט) מפחיתות את עוצמת ההתפרקות.
- *מהירות* ההתאוששות תלויה ברמת **האמון** הנוכחית, ולא בפעולה הספציפית שנבחרה.
- אי-פעולה מאריכה את ההתפרקות ומגדילה את הקושי של היום הבא.

---
## 4. יעדים

**לכל משימה.** השלם את האינסטינקט המוצג כרגע — להעיר, להאכיל, לטייל, להזיז, לשחק, לשמור, לנוח.

**לכל יום.** השלם את סט המשימות והיעדים של היום, ואז בחר להמשיך ליום הבא או להפסיק.

**מתמשך.** הגדל את האמון דרך אינטראקציה מתמדת ומקשיבה. האמון עובר בין כל הימים והוא מדד ההתקדמות האמיתי של המשחק.

**תגמול לשחקן.** השחקן מתוגמל על כך שהוא *רואה יותר* — מבחין במצב הבעלים, תופס רמזים עדינים, ומבין את העבודה הנסתרת של להיות חברו הטוב ביותר של האדם.

---

## 5. תהליכים

סדר התהליכים המרכזיים במשחק.

### 5.1 אתחול יום
1. מספר הימים מתעדכן ומוצג (יום 1, יום 2, ...).
2. אנרגיה ותיאבון של הכלב מתחדשים.
3. מצב הבעלים מחושב מתוצאת היום הקודם: מצב רוח בסיסי, תיאבון, ורצון להשתלב, ליצור קשר.
4. רשימת המשימות של היום מורכבת, ביחס למצב הבעלים ולפי מצב האנרגיה של הכלב.
5. התמונה נטענת בחדר השינה עם הכלב ליד המיטה.

### 5.2 מחזור הנחיות אינסטינקט *(הלולאה המרכזית)*
1. אינסטינקט מתעורר — הנחיה מופיעה על המסך (אייקון, שביל ריח, או הדגשה עדינה על אובייקט שניתן לאינטראקציה).
2. השחקן לוחץ על היעד המודגש.
3. העולם ננעל; UI המשימה נפתח.
4. השחקן מבצע את המשימה (קליק אחד, לחיצה בזמן, או מיני-משחק).
5. התוצאה נפתרת — הצלחה, חלקית, או החמצת זמן.
6. משוב מנגן: אנימציית תגובה של הבעלים, צליל, ושינוי באמון אם רלוונטי.
7. העולם משתחרר; הכלב חוזר לתנועה חופשית.
8. חזור עד שרשימת המשימות של היום אזלה.

### 5.3 עורר את הבעלים *(דוגמה מפורטת)*
1. הנחיה מדגישה את הבעלים במיטה.
2. השחקן לוחץ על הבעלים.
3. UI המשימה נפתח עם פעולת קפיצה.
4. השחקן לוחץ לקפוץ.
5. הבעלים זז; אם הלחיצה נעשית בתוך חלון הזמן, הבעלים קם מיד ומלטף את הכלב (+אמון).
6. אם הזמן החמצה, הבעלים מתגלגל והמשימה מופיעה שוב לאחר השהייה קצרה.
7. בהצלחה, האינסטינקט הבא מתעורר (ארוחת בוקר).

### 5.4 רצף הליכה
1. הנחיה מדגישה את הרצועה או הדלת.
2. השחקן לוחץ עליה; שלב הכנה קצר מתנגן (הבעלים לובש נעליים — איטי יותר אם מצב רוח הבעלים נמוך).
3. המצלמה עוברת מהתצוגה האיזומטרית הפנימית לתצוגת ההליכה.
4. השחקן נע לאורך מסלול קבוע; הכלב עשוי לרדוף אחרי משהו חסר תועלת כסטייה אופציונלית.
5. משך ההליכה ותגובתיות הבעלים משתנים לפי מצב הבעלים.
6. המעבר חוזר להצגה הפנימית; עלות אנרגיית הפעולה יורד.

### 5.5 פרק מנוחה
1. בין משימות, הכלב יכול לשכב.
2. זמן מנוחה רץ; השחקן יכול לסיים אותו מוקדם.
3. מנוחה מפחיתה את ההסתברות לאירועים שליליים מאוחר יותר ביום.
4. הימנעות ממנוחה לא חוסמת כלום — רק משפיעה על הסתברות האירועים.

### 5.6 פתרון משימה ומשוב
- **השלמה:** UI המשימה נסגר, הבעלים מגיב בחיוב, האמון עולה.
- **חלקי:** המשימה מושלמת אך ללא תגובת בונוס; אין רווח באמון.
- **החמצת זמן:** המשימה נשארת זמינה; תגובת הבעלים ניטרלית או מעט מזלזלת.
- **התעלמות:** הנחיית האינסטינקט נשארת או פגה; רשימת המשימות של היום מתארכת מעט.

### 5.7 אירוע התפרקות *(משבר רגשי)*
1. **הפעלה:** בסיס נמוך אקראי או הזנחה מצטברת על פני 2–3 ימים.
2. **מצב:** הבעלים נכנס למצב כיבוי (בכי, שקט, נייח). צלילים סביבתיים דועכים, צבעי הסביבה דהויים.
3. **הנחיה:** "נחם את הבעלים." מופיעות אפשרויות פעולה מרובות (ליטוף, ישיבה קרוב, נביחה רכה, הבאת שמיכה).
4. **פתרון:**
   - השחקן בוחר *כל* פעולה.
   - כל הפעולות מפעילות רצף אנימציית "הרגעה".
   - **אמון גבוה:** הבעלים נרגע מהר, מחבק את הכלב, מצב הרוח מתייצב.
   - **אמון נמוך:** הבעלים נרגע לאט, דוחה בעדינות או פשוט מפסיק לבכות, מצב הרוח משתפר מעט.
   - **אין פעולה:** ההתפרקות נמשכת, שואבת את שארית זמן היום ומגדילה את הקושי של היום הבא.
5. **תוצאה:** ההתפרקות מסתיימת. אין "בחירה שגויה". האמון עשוי לעלות מעט עבור כל ניסיון שנעשה.

### 5.8 סוף יום
1. כל המשימות נפתרו (הושלמו או פגו).
2. אינסטינקט שינה מופעל; השחקן מסמן שינה והכלב מתמקם.
3. סיכום היום נפתר בשקט — אין מסך ציונים, אין ציון.
4. מצב הבעלים נכתב קדימה ליום הבא.
5. השחקן מקבל שתי אפשרויות: **התחל את היום הבא**, או **צא**.

---

## 6. משאבים

המשאבים מחולקים בין שני הדמויות. האסימטריה הזו היא עמוד השדרה של העיצוב.

### משאבי הכלב — *מתחדשים באופן עצמאי*
| משאב | התנהגות |
|---|---|
| **אנרגיה** | מגבילה את מספר הפעולות ביום. מתרוקנת בתנועה, משחק והליכות. מתחדשת במלואה בכל לילה. |
| **תיאבון** | עולה במהלך היום; מסופק בארוחת בוקר וערב. משפיע על נכונות לבצע משימות. |

מוצגים כמדדים גלויים.

### משאבי הבעלים — *לא מתחדשים לבד*
| משאב | התנהגות |
|---|---|
| **מצב רוח** | מצב בסיסי המועבר בין ימים. עולה מטפול מקשיב, יורד מהזנחה או רמזים שגויים. |
| **תיאבון** | דורש מהכלב לדחוף. לא יתאושש ללא הנחיה. |
| **קשב** | מאגר מוגבל. דרישה יתר גורמת לנסיגה; שימוש נמוך גורם לסטייה. |

### משותף
| משאב | התנהגות |
|---|---|
| **קשר / אמון** | זיכרון קבוע של הקשר. עולה לאט, **לעולם לא מאפס**, ועובר בכל יום. |

**כלל הצגה:** שלושה מהם מוצגים כמדדים גלויים. השאר מועברים דרך התנהגות גופו של הכלב — תנוחת גוף, מיקום זנב, קצב, היסוס — כך שהשחקן קורא את החיה במקום ה-HUD.

---

## 7. קונפליקטים

**ראשי — שחקן מול סביבה.** הכלב רוצה לפעול; הבית, מזג האוויר, מצב הבעלים, והמגבלות של הכלב עצמו מתנגדים. דלתות סגורות. נעליים חסרות. הבעלים לא יקום. זהו החיכוך המכני של כל משימה.

**משני — אי-הבנה.** המנוע הקומי והרגשי. הכלב מפרש התנהגות אנושית דרך לוגיקה של כלב ומקבל טעויות בצורה מצחיקה, ואז עצובה. אנחה של הבעלים עשויה להיות עייפות או עיכול. הדלת הסגורה עשויה להיות דחייה או תנומה. השחקן פועל לפי הקריאה של הכלב, לא האמת.

**צד שלישי — אינסטינקט מול חובה.** הכלב רוצה לרדוף אחרי הסנאי. הכלב אמור לדחוף את הבעלים לאכול. שניהם דחפים לגיטימיים; רק אחד ברשימת המשימות.

**צד רביעי — משבר רגשי.** הקונפליקט כאן הוא לא "נכון מול שגוי" אלא "נוכחות מול היעדרות". האתגר הוא להישאר מעורב במהלך התפרקות, לדעת שכל מאמץ עוזר, אבל מהירות ההתאוששות תלויה בהיסטוריה של הקשר.

---

## 8. גבולות

**מרחבי.** פנים בית יחיד ותוואי הליכה קבוע בשכונה. אין עולם פתוח, אין יצירה פרוצדורלית. ויזואלית, הבית מורכב מרקעים ציוריים מפורטים המכסים כל אזור נפרד — המראה והצפיפות הרצויים דומים ל-*Shin-chan: Shiro and the Coal Town*.

**זמני.** יום אחד במשחק ממופה לפרק משחק אחד. ימים הם רציפים ומצטברים אך לעולם לא מתוזמנים מול השעון האמיתי.

**מכני.** הכלב לא יכול לדבר, לא יכול להשתמש בכלים, ולא יכול לחשוב באופן מופשט. כל אינטראקציה היא שפת גוף, קרבה, צליל, ונוכחות פיזית.

**טוני.** חם, חכם, תצפיתי. אין אכזריות, אין עונש, אין ייאוש. הקושי מבוטא כאיטיות ושקט, לעולם לא כעוינות.

**נרטיבי.** הסיפור נשאר מרומז. כלום לא מוסבר ישירות; השחקן משלים אותו מהתנהגות וסביבה.

**בטיחות רגשית.** במהלך אירועי התפרקות, המשחק מבטיח שאף פעולה שננקטה על ידי השחקן לא תחמיר את מצבו של הבעלים. הסיכון היחיד הוא אי-פעולה.

---

## 9. תוצאות

מכיוון שהמשחק לא יכול להיכשל באופן קבוע, התוצאות מדורגות לפי **כמה השחקן שם לב** — לא לפי האם הוא ניצח.

| תוצאה | איך זה נראה |
|---|---|
| **יום מלא** | כל המשימות הושלמו עם בונוסי זמן. הבעלים חיוני יותר באופן גלוי. רמת אמון עולה. תגובות נוספות ונקודות נרטיב קטנות אפשריות. |
| **יום שקט** | משימות הושלמו, זמנים החמצו ברובם. הבעלים מתפקד אך שטוח. האמון נשמר. כלום לא אבד. |
| **סטייה (Drift)** | מספר משימות הוזנחו או החמצו זמן. הבעלים נסוג מעט — ישן יותר, אוכל פחות, מעורב פחות. היום הבא מתחיל לאט יותר ודורש יותר מאמץ. **כלום לא הוחרם.** |
| **יום עמוק** | נדיר. הקשבה גבוהה לאורך ימים רבים מייצרת רגעים של הבנה הדדית אמיתית — הבעלים מגיב לכלב ללא הנחיה, וקריאות הכלב הופכות מדויקות יותר. |
| **פריצת דרך** | לאחר אירוע התפרקות, אם השחקן מנחם בהצלחה את הבעלים (אפילו בצורה מגושמת), אנימציית "הקשר העמיק" ייחודית מתנגנת, ומשחררת אינטראקציה עדינה חדשה לימים עתידיים. |

**על "כישלון":** שקיעה היא הקרוב ביותר שהמשחק מגיע אליו. זה לא מצב אובדן — זה הבעלים עובר תקופה רעה, ועבודת הכלב ממשיכה מחר. אמון לעולם לא יורד. אין התקדמות שנמחקה. השחקן לעולם לא נאמר לו שהוא עשה רע; הוא פשוט מבחין שהבית שקט יותר, והמשחק סומך עליו לדאוג.

**על התפרקויות:** אלו לא כישלונות אלא נקודות מפנה. הן בוחנות את נכונות השחקן להישאר נוכח. התוצאה היא תמיד התאוששות, אך *איכות* ההתאוששות תלויה בחוזק הקשר.

---

## 10. סקר משחקים

חיפושים בחנות Google Play ובאינטרנט הרחב עבור **"משחקים בסגנון Tamagotchi"** מחזירים כותרות שכמעט תמיד שומרות על השחקן בתפקיד המטפל של יצור. חיפוש נפרד עבור **"כלב מטפל באדון"** מחזיר רק וריאציות על *טיפול בכלב* — הפריסה ההפוכה לא נראית קיימת כמוצר מסחרי מושלם.

מתחרים קיימים — *Pou*, *My Forever Tamagotchi*, *Nintendogs*, *Neopets* — שונים מאינוממוטוצ'י בעיקר על ידי החלפת היצור המטופל, תוך שמירה על כיוון הטיפול זהה.

**ההבחנה של אינוממוטוצ'י היא ההיפוך עצמו:** השחקן הוא החיה, התלוי הוא האדם, והממשק הוא תפיסת הכלב במקום לוח בקרה. מעל זה משתלב טון חכם, סיפור מרומז שרץ בשקט ברקע, ומנגנון בטיחות ייחודי שבו אהבה לעולם אינה התשובה הלא נכונה.


</div>
