# GLD 模块

## 前言

鉴于项目之前的GLD模块常用于同步GLD数据，目前所使用的方法是总部出一套GLD配置文件的模板，程序通过这个模板创建出对应的gld数据结构配置文件，然后通过google excel同步工具使用程序代码对应不同配置文件结构写出序列化和反序列化代码，将数据写入到项目中的json文件，再通过程序解析json文件应用到游戏中。

这些步骤有以下几个问题：

1.总部的gld文件过于书面并不利于程序的识别与生成

2.总部的gld每次做修改后都需要有映射脚本映射到对应的程序识别的gld文件中

3.每次总部修改gld功能和结构，程序需要再重新修改序列化代码进行适配

这些问题导致的结果：

1.过于麻烦，程序需要根据代码需求重新整理结构逻辑。

2.程序需要根据配置文件的不同内容编写对应的数据类进行存储

3.gld同步过程痛苦，经常导致总部修改了gld而备份表中并未做修改

为此特实现了一套gld框架，为了解决这一系列问题。



## 新GLD框架

通过制定一系列GLD配置规则用于程序化识别配置文件结构，并通过配置文件结构动态生成json文件和对应的数据结构。并且总部以及项目组可以共用一套配置文件结构，省去了同步的麻烦。通过添加数据验证可以保证配置文件内容的可靠性，不利于出错。程序无需再实现序列化内容，程序可以更关注数据结构如何制定，然后直接序列化出来即可在游戏中使用，大大省去了程序开发时间，同时也更加便于修改。

### 使用方法：

1.导入资源加载模块包

2.导入GLD模块包

3.在项目立项之初点击TC->GLD->Create Download Config Template按钮，可以在项目中创建一个名为DownloadConfig的json文件，用于配置新的Gld配置数据。格式如下：

```json
{
    "downLoadList": [
        {
            "id": 1,
            "name": "TestConfig",
            "url": "https://docs.google.com/spreadsheets/d/e/2PACX-1vTpsZWEm73i6BeNpCkS_6zTfhsddSk6jxBpXZW9sPPPeBO24q_FHyX15NDdjESrNLW88KY5J9-Fp6pO/pub?gid=216506083&single=true&output=csv",
            "jsonPath": "_res/4 cfg/1 challenge/1 TestConfig.json",
            "csPath": "_NewGame/Scripts/GLD/TestConfig.cs"
        }
    ]
}
```

其中：

id -  唯一id

name - 对应的配置文件名称

url - google excel 发布到网络的csv文件的链接地址

jsonPath - 同步后的json文件保存的目录地址

csPath - 同步后生成的cs文件保存的目录地址



对应的配置文件配置如下：

| #注释 | TestConfig |         |         |        |              |           |             |
| ----- | :--------- | ------- | ------- | ------ | ------------ | --------- | ----------- |
| #注释 |            |         |         |        |              |           |             |
| int   | string     | bool    | resid   | float  | list<string> | list<int> | list<float> |
| id    | name       | Is_null | res     | number | tags         | values    | numers      |
| 1     | aaa        | FALSE   | 1\|1\|1 | 1.2    | AAA          | 2         | 1.5         |

1.其中#注释代表注释内容，程序不识别对应的注释内容，只需要在第一列第一个字符为#，程序会自动忽略其对应的内容

2.第三行的类型为对应程序脚本的类型，目前支持只有以上8种类型。

3.第四行为对应的脚本变量名称，用于生成脚本的变量名称，序列化后会生成一个cs类用于定义。

4.第五行为对应的数据类型，除了resid为需要1|1|1此配置方式以外其他的均正常配置即可。

**注意：其中resid为资源ID的自定义类型，只用于资源加载中对应的资源位置id。list类型为列表，需要占用多列，需要在list里面有多个元素可以在后面添加多列只需要类型和变量名称一致即可**



通过上面表配置序列化出来的数据结构如下：

```csharp
/**************************************************************************************************
                                   自动生成代码  请勿手动修改
**************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestConfig : ConfigBase
{
   public List<TestConfig_Data> _datas = new List<TestConfig_Data>();
}

[System.Serializable]
public class TestConfig_Data : ConfigData
{
   public int id;
   public string name;
   public bool Is_null;
   public ResId res;
   public float number;
   public List<string> tags;
   public List<int> values;
   public List<float> numbers;
}
```



### 同步方式

当在excel中配置完成，并且在DownloadConfig中配置过后，开启小飞机全局模式，点击TC->GLD->DownLoad->Test Download TestConfig. 即可看到数据同步下来，也可以再对应的目录下查看同步下来的json文件以及对应的cs脚本情况。



### 游戏中使用数据

```csharp
TestConfig config = GLDCfgMgr.Instance.GetConfigData<TestConfig>(ResId.Gen(4, 1, 1));
```

其中config为序列化后的数据结构。



### 优缺点

1.省事，省去了序列化的步骤。

2.结构支持不全面，有些数据不好配

2.同步数据时同步代码，在多人合作修改同一份数据表时会造成冲突，不好处理。