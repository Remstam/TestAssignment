﻿## Настройки игры
### Player
Управляемый персонаж<br>
**Конфиг**: Assets/Resources/ScriptableObjects/PlayerConfig

| Настройка                           | Что означает                          |
|-------------------------------------|---------------------------------------|
| Move Speed                          | Скорость передвижения (м/c)           |
| Rotation Speed                      | Скорость поворота (град/c)            |
| Health                              | Число очков здоровья                  |
| Attack                              | Число очков атаки                     |
| Defence                             | Уровень защиты (от 0 до 1)            |
| Hit Cooldown                        | Пауза между атаками в ближнем бою (с) |

### Enemies
Враги на игровой арене

#### Cylinder
Враг-цилиндр, перемещается медленее и имеет больший запас очков здоровья<br>
**Конфиг**: Assets/Resources/ScriptableObjects/EnemyCylinder

#### Sphere
Враг-сфера, перемещается быстрее и имеет меньший запас очков здоровья<br>
**Конфиг**: Assets/Resources/ScriptableObjects/EnemySphere

| Настройка                           | Что означает                          |
|-------------------------------------|---------------------------------------|
| Enemy Name                          | Название врага                        |
| Prefab Info                         | Информация о префабе врага            |
| Health                              | Число очков здоровья                  |
| Move Speed                          | Скорость передвижения (м/c)           |
| Attack                              | Число очков атаки                     |
| Defence                             | Уровень защиты (от 0 до 1)            |
| Hit Cooldown                        | Пауза между атаками в ближнем бою (с) |
| Description                         | Описание                              |

#### Prefab Info
Содержит информацию о создаваемом префабе<br>
**Конфиг**: нет

| Настройка | Что означает                               |
|-----------|--------------------------------------------|
| Prefab    | Ссылка на префаб                           |
| Pool Type | Тип пула, которому принадлежит этот префаб |

#### EnemySpawner
Настройки для создания врагов на арене<br>
**Конфиг**: Assets/Resources/ScriptableObjects/EnemySpawnerConfig

| Настройка        | Что означает                                                         |
|------------------|----------------------------------------------------------------------|
| Enemy Configs    | Список конфигов врагов (Enemies)                                     |
| Enemies On Board | Число врагов, которое может одновременно находиться на игровой сцене |

### Spells
Заклинания, которыми обладает игрок (Player)

Все заклинания имеют следующие настройки:

| Настройка | Что означает                                    |
|-----------|-------------------------------------------------|
| Cooldown  | Пауза (с) между повторными запусками заклинания |

#### BulletSpell
Игрок выпускает пулю. Попадая во врага, она наносит урон и исчезает с игровой сцены.<br>
**Конфиг**: Assets/Resources/ScriptableObjects/Spells/BulletSpellConfig

| Настройка   | Что означает                                                                                       |
|-------------|----------------------------------------------------------------------------------------------------|
| Prefab Info | Информация о префабе пули                                                                          |
| Damage      | Число очков урона                                                                                  |
| Life Time   | Время жизни (с) пули. Если пуля не попадает в кого-либо за это время, она исчезает с игровой сцены | 
| Speed       | Скорость передвижения (м/с)                                                                        |

#### CanonBall
Игрок выпускает пушечное ядро. По истечении своего срока жизни оно "взрывается" (исчезает с игровой сцены) и наносит определенный урон всем врагам в определенном радиусе<br>
**Конфиг**: Assets/Resources/ScriptableObjects/Spells/CanonBallConfig

| Настройка    | Что означает                        |
|--------------|-------------------------------------|
| Prefab Info  | Информация о префабе пушечного ядра |
| Damage       | Число очков урона                   |
| Life Time    | Время жизни (с) пушечного ядра      | 
| Speed        | Скорость передвижения (м/с)         |
| DamageRadius | Радиус поражения (м)                |

#### Doppelganger
Игрок выпускает своего двойника. Все враги переключаются на атаку двойника (или на случайно выбранного двойника, если их несколько на игровой сцене), пока не убьют его. Двойник не может двигаться и атаковать.<br>
**Конфиг**: Assets/Resources/ScriptableObjects/Spells/DoppelgangerConfig

| Настройка    | Что означает                                                            |
|--------------|-------------------------------------------------------------------------|
| Prefab Info  | Информация о префабе двойника                                           |
| Health       | Число очков здоровья                                                    |
| Limit        | Число двойников, которое может одновременно находиться на игровой сцене | 

#### MindShatter
Вызывает у всех врагов помутнение разума. Все враги получают случайным образом точку на игровой сцене, к которой начинают двигаться. Ближняя атака врагов на время действия заклинания отключается.
Если враг достиг точки назначения до того, как время заклинания вышло, то он остается там до того момента, пока время заклинания не выйдет.
Во время действия заклинания игрок может наносить любой урон по врагам. Заклинание имеет приоритет перед заклинанием двойника, т.е. при одновременном действии обоих заклинаний для врага приоритетнее данное заклинание.<br>
**Конфиг**: Assets/Resources/ScriptableObjects/Spells/MindShatterConfig

| Настройка    | Что означает                                             |
|--------------|----------------------------------------------------------|
| Points Count | Количество точек назначения, генерируемых для заклинания |

### Utils
Дополнительные конфиги, используемые в игре

#### PoolConfig
Характеристики для пулов объектов, которые могут быть созданы на сцене. Если пул переполнен, то попытка возвратить объект в пул приведет к удалению этого объекта с игровой сцены. Изначально каждый пул пустой и заполняется по мере гибели объектов на сцене.<br>
**Конфиг**: Assets/Resources/ScriptableObjects/PoolConfig

| Настройка | Что означает                                                                           |
|-----------|----------------------------------------------------------------------------------------|
| Pool Type | Тип пула                                                                               |
| Size      | Размер пула                                                                            |
| Position  | Позиция (Vector3) для якоря пула на сцене, под который будут возвращаться объекты пула |

#### DebugConfig
Конфиг для различного дебага
**Конфиг**: Assets/Resources/ScriptableObjects/DebugConfig

| Настройка           | Что означает                                                                   |
|---------------------|--------------------------------------------------------------------------------|
| Random Enemy Damage | Урон, который будет нанесен случайному врагу на сцене при нажатию на клавишу R |