echo_run()
{
    # 黄色カラーでログ出力
    echo -e "\e[33m $1 \e[m"
}

CURRENT_DIR=$(cd $(dirname $0);pwd) # スクリプトのディレクトリ取得
PARENT_DIR=$(cd "$(dirname "$CURRENT_DIR")" && pwd) # 一つ上のディレクトリ取得
MASTER_DIR=$PARENT_DIR/Assets/Scripts/Master/
GENERATED_DIR=$PARENT_DIR/Assets/Scripts/Generated/
NAMESPACE="Example"

# エラーが発生したら即終了
set -e
echo_run マスターメモリ関連のデータ作成中....

# マスターメモリ関連のデータ作成
dotnet-mmgen.exe -i $MASTER_DIR -o $GENERATED_DIR -n "Example" -addImmutableConstructor

echo_run "\nメッセージパック関連のデータ作成中...."

# メッセージパック関連のデータ作成
mpc -i $MASTER_DIR -o $GENERATED_DIR

echo_run =====実行完了=====
set +e