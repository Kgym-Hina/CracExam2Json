# CracExam2Json

将中国业余无线电协会的题库 `txt` 文件转换为 `json` 的简单工具

## Usage

```
dotnet run
```

## 注意

官网下载的题库为 GBK2312 编码，本项目默认以 UTF8 编码读取。如果要指定编码，可以添加 `--gb2312` 或 `-g` 参数。
