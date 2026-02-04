# TODO

This file tracks **foundational tasks** required to build a stable, scalable 3D game world.
It intentionally avoids feature creep and content bloat.

If a task does not support long-term iteration or shipping, it does not belong here.

---

## 0. Project Baseline

- [x] Confirm engine version (Unity)
- [x] Lock target platforms (PC first)
- [x] Create clean Git repo
- [x] Add .gitignore for Unity
- [x] Verify project opens and runs cleanly

---

## 1. Camera & Presentation (First, Always)

- [x] Choose camera type (aarpg style)
- [?] Lock camera angle **(Subject to change)**
- [?] Lock camera distance **(Subject to change)**
- [ ] Decide if camera rotates or remains fixed
- [ ] Establish visual readability from gameplay height
- [x] Add temporary player proxy for scale testing

> No asset work proceeds until the camera feels correct.

---

## 2. World Scale & Grid Rules (Non-Negotiable)

- [ ] Confirm scale rule (1 Unity unit = 1 mßeter)
- [ ] Decide grid size for modular assets
- [ ] Define standard wall height
- [ ] Define standard door dimensions
- [ ] Define stair rise/run rules
- [ ] Create scale reference prefabs (door, character, step)

> These rules become law once set.

---

## 3. Core Modular Environment Kit (Minimal Viable Set)

### Floors
- [x] Base floor tile
- [ ] Floor variant (damaged / worn)
- [ ] Path or transition tile

### Walls
- [x] Straight wall
- [ ] Inner corner wall
- [ ] Outer corner wall
- [ ] Doorway wall
- [ ] Half / broken wall

### Vertical Movement
- [ ] Staircase (up)
- [ ] Staircase (down or mirrored)
- [ ] Ramp (if applicable)

> Goal: build full rooms using **only** these assets.

---

## 4. Structural & Support Assets

- [x] Pillar (round or square)
- [ ] Support beam
- [ ] Archway
- [ ] Railing or ledge blocker
- [ ] Overhang / ceiling piece

---

## 5. Doors & Gating

- [ ] Standard door
- [ ] Locked door variant
- [ ] Door frame
- [ ] Open doorway
- [ ] Gate or barrier asset

---

## 6. Core Props (World Credibility)

### Structural Props
- [ ] Pillar variant
- [ ] Fence or barrier
- [ ] Support detail

### Clutter Props
- [ ] Crate
- [ ] Barrel
- [ ] Debris pile
- [ ] Broken prop variant

> These exist to prevent empty-feeling spaces.

---

## 7. Interaction Objects (Visual First)

- [ ] Chest (closed)
- [ ] Chest (open)
- [ ] Lever or switch
- [ ] Pressure plate
- [ ] Shrine / altar placeholder

Logic comes later. Visual presence comes first.

---

## 8. Lighting Sources (Physical Objects)

- [ ] Torch (wall-mounted)
- [ ] Torch (standing)
- [ ] Lantern
- [ ] Magical light source
- [ ] Light prefab with flicker logic

Lights should always have a visible source.

---

## 9. Nature & Terrain (If Applicable)

- [ ] Ground terrain piece
- [ ] Rock (small)
- [ ] Rock (large / blocker)
- [ ] Tree
- [ ] Bush / foliage cluster

---

## 10. Technical Assets (Invisible but Required)

- [ ] Simplified collision meshes
- [ ] Invisible boundary walls
- [ ] NavMesh setup
- [ ] NavMesh modifiers
- [ ] Occlusion blockers
- [ ] LOD variants (if needed)

---

## 11. Prefabs & Organization

- [ ] Convert environment pieces into prefabs
- [ ] Ensure pivots are correct for snapping
- [ ] Verify scale consistency across assets
- [ ] Clean up naming conventions
- [ ] Validate folder structure

---

## 12. Vertical Slice (Proof of Concept)

- [ ] Build one complete room
- [ ] Connect rooms via hallway
- [ ] Add door gating
- [ ] Add vertical element (stairs or platform)
- [ ] Light the space intentionally
- [ ] Add clutter and props
- [ ] Walk through as player

If this slice feels good, the project is viable.

---

## 13. Review & Lock-In

- [ ] Validate camera readability
- [ ] Validate navigation flow
- [ ] Validate asset reuse
- [ ] Identify missing “always-present” assets
- [ ] Update README with lessons learned

---

## Notes
This TODO list is expected to evolve.
However, **core rules and scale decisions should not**.
