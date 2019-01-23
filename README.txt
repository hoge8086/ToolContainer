新規にプラグインを作成する場合は、プロジェクト名を「～Plugin」としクラスライブラリで作成する。
（ToolContainerは「～Plugin.dll」という名前のDLLをロード対象とするため）

プラグインのプロジェクトでは、以下DLLを参照し、IPluginインタフェースを実装した公開クラスを実装すること。
「ToolContainer\PluginInterface\bin\Release\PluginInterface.dll」

作成したプラグインのDLLを、ToolContainerのリリースフォルダに以下の構成で格納する。
ビルド後イベントに以下を追加する。
「xcopy /I /Y $(ProjectDir)bin\$(ConfigurationName) $(SolutionDir)..\$(ConfigurationName)\plugins\$(ProjectName)」

Release\
　├ ToolContainer.exe
　├ plugins\
　│　├ <プロジェクト名>\
　│　│　├ <プロジェクト名>.dll
　│　│　└　必要モジュール（※PluginInterface.dllは不要）
　│　│　
　│　└ 他のプラグイン
　└ etc
「ToolContainer.exe」が自動で、dllを認識してプラグインがロードされる。
