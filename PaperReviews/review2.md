# *Asymmetric Lateral Field-of-View Restriction to Mitigate Cybersickness During Virtual Turns* (IEEE VR, 2022)

**Citation**  
F. Wu and E. S. Rosenberg, "Asymmetric Lateral Field-of-View Restriction to Mitigate Cybersickness During Virtual Turns," *2022 IEEE Conference on Virtual Reality and 3D User Interfaces (VR)*, Christchurch, New Zealand, 2022, pp. 103-111, doi: 10.1109/VR51125.2022.00028. keywords: {Three-dimensional displays;Cybersickness;Navigation;Design methodology;Conferences;Virtual environments;Games;Human-centered computing;Human computer interaction (HCI);Interaction paradigms;Virtual reality;Human-centered computing;HCI design and evaluation methods;User studies},

---

### Review

Field-of-view “tunneling” is a familiar comfort aid in VR, but most implementations are **symmetric**. You get a vignette on both sides during turns. This paper proposes a small, smart tweak: **asymmetric lateral restriction**. During a virtual turn, the system masks only the *outer* side and **shifts the mask toward the turn direction**, preserving visibility on the inside of the turn where the user naturally looks.

The study (remote, Quest/Quest 2, N≈90) compares **Side (asymmetric)** vs. **Symmetric** vs. **None** while people navigate a turn-heavy maze. Results land where intuition points: Side **reduces cybersickness and discomfort** versus None, **beats Symmetric on subjective visibility**, and supports **longer time-in-VE** without observable penalties. Presence doesn’t budge much—consistent with mixed findings in the comfort literature—but that’s not the point; the aim here is stability, not awe.

Strengths first. The intervention is **simple to implement**—a shader-driven mask with a modest **lateral shift** and velocity-linked scaling—and the paper provides enough parameterization to replicate. The authors also explain the **perceptual rationale** crisply: during a turn, users bias gaze toward the intended path; why obscure that region?

Caveats: **Remote testing** trades control for scale. Lighting, posture, and seating varied, which can smear effects in locomotion research. The task is also **turn-dominant**; translation-heavy or mixed locomotion may show different trade-offs. And while Side helps with visibility, it still **occludes peripheral flow**, which some players use to judge motion speed—tuning matters to avoid a “blinder” feel.

What I’d adopt in my own builds: use **asymmetric restrictors for smooth turning by default**, with an **ease-in/out** tied to angular velocity to avoid popping. Keep a **user toggle** (None / Sym / Side) in options and log quit rates and comfort ratings; the paper’s effects are large enough that you should see differences in practice. For translation, symmetric FOV reduction can still play a role, but for turns the **asymmetric variant is the better ergonomic default**.

Overall, the paper contributes a **low-complexity, high-leverage change** to a well-worn comfort tool. It’s the kind of tweak that doesn’t add friction to design and meaningfully improves player tolerance in turn-heavy scenes.

---

**Author’s Note:**  
Generative AI tools (ChatGPT, GPT-5) were used only for grammar, structure, and formatting improvements.  
All analysis, critique, and interpretation of the paper are my own.
