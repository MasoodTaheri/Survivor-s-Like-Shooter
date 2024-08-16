I used:
- object pooling for bullets,  each enemy type, and each loot.

- scriptable objects for enemies
- scriptable objects for enemies controller
- scriptable objects for loot manager
- scriptable objects for player gun and movement

- enemies are kept in an "object with a chance" that is accessible via an enemies controller scriptable object.

- loots used an abstract class and each one Implemented its own version of loot so it is easy to extend and add new loots (OCP)

- each class that needs any other class references, will get it through its manager (dependency inversion) and managers will get their dependencies through the Zenject.



