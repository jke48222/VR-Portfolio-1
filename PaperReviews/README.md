# Paper Reviews

This folder contains two analytical reviews of recent virtual reality research papers. Each review examines a specific contribution to VR interaction or comfort design and connects the findings to applications within my own Unity VR projects.

---

## Review 1 — *IntenSelect+: Enhancing Score-Based Selection in Virtual Reality* (IEEE TVCG, 2024)
**Citation:**  
M. Krüger, T. Gerrits, T. Römer, T. Kuhlen, and T. Weissker, *“IntenSelect+: Enhancing Score-Based Selection in Virtual Reality,”* IEEE Transactions on Visualization and Computer Graphics, vol. 30, no. 5, pp. 2829–2838, 2024.  
[DOI: 10.1109/TVCG.2024.3372077](https://doi.org/10.1109/TVCG.2024.3372077)

**Summary:**  
This paper improves temporal score-based selection in VR by separating growth and decay parameters and supporting selection of geometric primitives such as lines and surfaces. These refinements produce smoother, more accurate pointing in complex 3D scenes.

**Relevance:**  
Its focus on geometry-based selection influenced how I implemented precision grabbing in Demo 4’s potion puzzle. Defining interaction zones as shapes rather than points makes object manipulation more reliable and natural.

---

## Review 2 — *Asymmetric Lateral Field-of-View Restriction to Mitigate Cybersickness During Virtual Turns* (IEEE VR, 2022)
**Citation:**  
F. Wu and E. S. Rosenberg, *“Asymmetric Lateral Field-of-View Restriction to Mitigate Cybersickness During Virtual Turns,”* IEEE Conference on Virtual Reality and 3D User Interfaces (VR), 2022, pp. 103–111.  
[DOI: 10.1109/VR51125.2022.00028](https://doi.org/10.1109/VR51125.2022.00028)

**Summary:**  
The authors present an asymmetric FOV reduction that masks only the *outer* edge during virtual turns, maintaining inner visibility and reducing cybersickness. The approach offers better comfort without sacrificing situational awareness.

**Relevance:**  
I applied this concept conceptually to Demo 3’s smooth-turn locomotion, where asymmetric visibility could improve user comfort and maintain immersion during movement-heavy gameplay.

---

## Folder Contents
```
PaperReviews/
│
├── review1.md   # IntenSelect+ review
├── review2.md   # Asymmetric FOV Restriction review
└── README.md
```

**Unity Version:** 6000.0.57f1  
**Author:** Jalen Edusei  
*Generative AI was used only for grammar and formatting. All analysis and interpretation are original.*
