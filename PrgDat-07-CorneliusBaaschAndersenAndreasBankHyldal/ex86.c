int choose(int n) {
    switch (n) {
    case 2: {
            int result;
            result = 42;
            return result;
    }
    case -1: {
            return 7;
    }
    }
    return -1;
}

void main() {
    int x;
    x = 0;

    switch (x = x + 1) {
    case 1: { print 11; }
    case 2: { print 22; }
    }
    print x;

    switch (99) {
    case 1: { print 99; }
    }
    print 33;

    print choose(2);
    print choose(-1);
    print choose(99);
}