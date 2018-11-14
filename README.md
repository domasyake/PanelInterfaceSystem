PanelInterFaceSystem
====

UGUIのパネルの操作に特化したEventSystem<br>
<br>
This is an EventSystem specialized for operation of the UGUI panel.

## Description
メニュー画面のような、振る舞いの異なる様々なパネルが混在している状況で操作対象を切り替えていく制御を簡単に行うことができる。<br>
座標ではなくHierarchy上での列挙順のみに依存するためパネル同士の位置関係を気にしなくてよい。<br>
Unity標準のEventSystemと比べパネルに特化しているため取り回しに優れる。<br>

It is possible to easily execute control to switch the operation target when various panels having different behaviors such as a menu screen are mixed.<br>
The system depends only on the enumeration order on the Hierarchy without depending on coordinates. So you do not have to worry about the positional relationship between the panels.<br>
Compared to the Unity standard EventSystem, it is easy to use because it is specialized in panels.<br>


## Demo
![demo](https://raw.githubusercontent.com/wiki/domasyake/PanelInterfaceSystem/images/sample_gif.gif)
## Requirement

[UniRx](https://github.com/neuecc/UniRx)

## Usage
同梱のサンプルが幾つかのユースケースを示している。<br>
The bundled example shows several use cases.

**ISelectablePanel**<br>
　このシステムで操作されるパネルは全てこのインターフェースを実装する必要がある。<br>
　システムから操作が行われたときに各メソッドが呼ばれる。<br>
　パネルが制御を行うのは推奨されない。<br>
<br>
　You need to implement this interface on all the panels operated on this system.<br>
　Each method is called when an operation is performed from the system.<br>
　It is not recommended that the panel control.<br>
<br>
**PanelEventSystem**<br>
　制御システム。同時に複数種のパネルを制御することはできないので、複数のシステムの同時稼働が可能となっている。<br>
　使用時には対象のパネルをアクティブにし、起動するという2ステップを踏む必要がある。<br>
　その他に再起動やパネルの追加などのメソッドが用意されているので要コード参照<br>
<br>
　This is a control system.This system can not control multiple kinds of panels at the same time. Therefore, it is possible to operate multiple systems at the same time<br>
　When you use it you need to take two steps of activating the target panel and starting it.<br>
　In addition, methods such as restart and addition of panels are prepared. So please refer to the code.<br>
<br>
**BasePanelsController**<br>
　PanelEventSystemからイベントを受け取り処理を行うコントローラのテンプレート<br>
　パネルには制御ロジックを持たせず、パネルのプロパティ等を参照して処理はコントローラで行うべきである。<br>
　このコントローラを使用せず、独自実装してもよい。<br>
<br>
　This is a template of the controller that receives events from PanelEventSystem and processes them.<br>
　The panel should not have control logic. The controller should perform processing such as transition and display by referring to properties of the panel.<br>
　You may implement your own implementation without using this controller<br>
<br>
## Install
1.Releseからunitypackageをダウンロード。 <br>
2.Unityにインポート。<br>
(3.AssetStoreからUniRxをインポート。)<br>
<br>
1.Download unitypackage from Relese.<br>
2.Import packages to Unity.<br>
(3.Import UniRx from AssetStore.)<br>
<br>
## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[domasyake](https://github.com/domasyake)