―――――――――――――――――――――――――――――――――――――
艦娘ドロップ記録 - かんろく -  説明書
													Ver.1.00 ぺやんぐ(peyangu485)
―――――――――――――――――――――――――――――――――――――

このツール(かんろく)は、ドロップした艦娘名を入力することでドロップ情報を保持するツールです。
イベント海域などでの掘り作業の記録の補助をすることが目的です。
機能は、ドロップ情報のリスト表示、Excel出力、ドロップ情報ツイートです。

■機能説明
・ドロップ情報のリスト表示
　ドロップした艦娘名をテキストボックスに入力後、Enterキー押下で画面下のリストに表示されます。(入力できるからって、大和とか初風大量捕獲とかしないように)
　リストの中で艦娘を選択した状態で、ドロップ削除ボタンを押下すると選択したドロップ情報を削除します。
　リストリセットボタンでリストの初期化が可能です。
　ツールを閉じても、次回起動時には前回のドロップ情報を表示します。
　なので、掘り終わった場合はリストリセットボタンでリストの初期化を行ってください。
　リストに表示される時、その艦娘のレアリティに応じて対象行の色が変わります。(白背景、銀背景、金背景、虹背景に対応)
　
・Excel出力
　出力ボタンを押下すると、CSV形式で現在のドロップ情報のリストを出力します。
　出力内容は、勝利判定と艦娘名です。（ドロップ回数は出力されません。)
　
・ドロップ情報ツイート
　ドロップ情報の追加時に、twitterアカウントと連携している場合はドロップ情報がツイートされます。
　アカウント連携は、リストリセットボタンの右にあるtwitterのアイコンをクリックすることで連携画面が表示されますので、そちらの画面の指示に従って、
  アカウント連携を行ってください。
＊アカウント連携について
　認証ボタンを押下すると、ブラウザが立ち上がりアプリ連携をするかどうか聞かれるので許可してください。
　その際にPINコードが表示されますので、ツール側のテキストボックスにコピペor入力し、完了ボタンを押下してください。
　認証完了のメッセージが出れば、連携完了です。
　出ない場合は、連携に失敗していますのでexeと同階層にあるLogフォルダのLogファイルのエラー内容とともにご一報ください。
　認証解除ボタンで、認証を解除することができます。
　認証はツールを閉じても保持されたままになるので、切りたいときは認証解除ボタンで解除してください。
　


みなさんの提督ライフの一助になることを願って。

※他のゲームでも使用可能です。(ただし、レアリティに応じて背景色が変わる機能は使えません)

GitHubにてソースコードを公開しています。
https://github.com/peyangu/KanmusuDrop

使用したライブラリ
・MetroRadiance(https://github.com/Grabacr07/MetroRadiance)
・Hammock(https://github.com/danielcrenna/hammock)
・TweetSharp(https://github.com/danielcrenna/tweetsharp)
・TweetSharp-Unofficial(https://www.nuget.org/packages/TweetSharp-Unofficial/)
・Json.NET(https://github.com/JamesNK/Newtonsoft.Json)

著作権・ライセンス表示はAUTHORS.txtに。

動作環境
・Win8.1,Win7での動作を確認しています。

開発環境・言語
WPF + C#、VisualStudio2013 Proで開発を行いました。
.NET FrameWorkは4.5です。