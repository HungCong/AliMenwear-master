# project_sem_3



## Clone project

```bash
$ git clone https://github.com/hocgia/project_sem_3.git
```

Sau khi clone project về thì chạy lệnh sau để install package:

```bash
$ Update-Package -reinstall
```

Nếu có lỗi thì tiếp tục chạy lệnh:

```bash
$ Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```

(khi update dữ liệu nên xóa các migration cũ đi, tạo mới migration)

Tạo migration

```bash
$ Add-Migration ten-migartion
```

Update database

```bash
$ Update-Database
```



## Lưu ý khi làm việc với git

### Quy định chung

- Tuyệt đối không push code thẳng lên master.
- Mỗi tính năng sẽ được code trên 1 branch mới.
- Chỉ bắt đầu code khi đã checkout sang branch tương ứng.

### Quy tắc đặt tên branch

```
f#tên-task
```

- Trong đó tên branch gồm: phần số của task trên master + tên tính năng đang code
- Tên branch viết bằng chữ thường ngăn cách bằng dấu gạch ngang (-)

### Một vài quy trình làm việc với git

- Tạo một branch mới.

  - Checkout sang master (trong trường hợp đang ở branch khác):

    (Lưu ý: Trước khi checkout cần phải commit các thay đổi ở branch hiện tại)

    ```bash
    $ git checkout master
    ```

  - Pull code mới từ master về:

    ```bash
    $ git pull origin master
    ```

  - Tạo 1 branch mới và checkout sang branch này:

    (Lưu ý: tên branch được đặt theo quy ước ở trên)

    ```bash
    $ git checkout -b <tên_branch>
    ```

- Pull code

  ```bash
  $ git pull origin <tên_branch>
  ```

- Push code

  > Lưu ý: 
  >
  > - Chỉ add các file cần thiết, không add tất cả ``$ git add .``
  >
  > - Đặt tên commit có ý nghĩa.

  ```bash
  $ git add <tên_file>
  $ git commit -m "tên commit"
  $ git push origin <tên_branch>
  ```

- Merge branch.

  - Để merge một branch bất kì vào branch hiện tại thì sử dụng cú pháp sau:

  ```bash
  $ git merge <tên_branch>
  ```

  - Giả sử bạn đang ở branch master, bây giờ muốn merge branch f#task1 vào branch master thì làm như sau:

  ```bash
  $ git merge f#task1
  ```

  - Giả sử bạn đang ở branch f#task1, muốn merge branch f#task2 vào branch master thì làm như sau:

  ```bash
  $ git checkout master
  $ git merge f#task2
  ```

- Xử lý conflict khi merge

  - Giả sử bạn làm hai task trên cả hai branch f#task1 và f#task2 và cả hai đều cùng sửa một file, lúc này khi merge f#task1 vào f#task2 sẽ bị xung đột (*conflict*), vì vậy bạn sẽ phải thực hiện sửa xung đột đó thì thao tác merge mới hoàn thành 100%.

    (Tạo và xư lý conflict)

  - **Bước 1**: Tại branch master bạn hãy tạo một file `demo.txt`, sau đó tạo thêm 2 branch **branch1** và **branch2.**

    **Tạo 2 branch**

    ```bash
    $ git branch branch1
    $ git branch branch2
    ```

    **Bước 2**: Chuyển sang làm việc tại branch1, sửa nội dung file demo.txt thành "*Xin chào, đây là demo branch1*" và thực hiện commit.

    ```bash
    $ git checkout branch1
    $ git add .
    $ git commit -m "Commit file demo branch1"
    ```

    **Bước 3**: Chuyển sang làm việc tại branch2, sửa nội dung file demo.txt thành "*Xin chào, đây là demo branch2*" và thực hiện commit.

    ```bash
    $ git checkout branch2
    $ git add .
    $ git commit -m "Commit file demo branch2"
    ```

    Như vậy cả 2 branch trên ta đều sửa chung một file demo. Bây giờ ta sẽ merge branch1 vào branch2 nhé.

    ```bash
    $ git merge branch1
    Auto-merging demo.txt
    CONFLICT (content): Merge conflict in demo.txt
    Automatic merge failed; fix conflicts and then commit the result.
    ```

    Như vậy nó đã báo là bị xung đột file `demo.txt`, tức là conflict file `demo.txt`. 

    Bây giờ ban mở file `demo.txt` lên thì sẽ thấy nội dung của nó như sau:

    **Nội dung file demo.txt**

    ```tex
    <<<<<<< HEAD
    Xin chào, đây là demo branch2
    =======
    Xin chào, đây là demo branch1
    >>>>>>> branch1
    ```

    Đoạn bị xung đột được bắt đầu bằng `<<<<<<< HEAD` và kết thúc tại `>>>>>>> branch1`, được ngăn cách bởi đường `=======`. Trong đó đoạn trên là của branch hiện tại (*branch2*) và đoạn dưới là của branch cần merge (*branch1*).

    Nhiệm vụ bây giờ của bạn là xem xét nội dung bị conflict đó xem cần sửa chỗ nào, lấy đoạn nào, sau đó xóa đi những ký hiệu trên. Giả sử mình muốn lấy cả 2 thì lúc này mình sẽ sửa file `demo.txt` thành:

    **Sửa file demo.txt**

    ```tex
    Xin chào, đây là demo branch2
    Xin chào, đây là demo branch1
    ```

    Ok, bây giờ ta cần commit để hoàn thành thao tác merge.

    ```bash
    $ git add .
    $ git commit -m "Xu ly conflict"
    ```

    Như vậy là ta đã xử lý conflick thành công.

