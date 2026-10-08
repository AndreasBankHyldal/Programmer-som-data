void main() {
    int i;
    int r;
    int arr[3];

    i = 0;
    print ++i;
    print i;
    print --i;
    print i;
    println;

    arr[0] = 10;
    arr[1] = 20;
    arr[2] = 30;

    r = ++arr[++i];
    print r;
    print i;
    print arr[0];
    print arr[1];
    print arr[2];
    println;

    i = 2;
    r = --arr[--i];
    print r;
    print i;
    print arr[0];
    print arr[1];
    print arr[2];
    println;
}