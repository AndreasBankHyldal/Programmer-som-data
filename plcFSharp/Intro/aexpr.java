import java.util.Map;

public abstract class Aexpr {
    public abstract String toString();
    public abstract int eval(Map<String, Integer> env);
    public abstract Aexpr simplify();

    static boolean isCst(Aexpr e, int n) {
        return e instanceof CstI && ((CstI) e).i == n;
    }

    // 1.4 (ii)
    public static void main(String[] args) {
        Aexpr e1 = new Sub(new Var("v"), new Add(new Var("w"), new Var("z")));
        Aexpr e2 = new Mul(new CstI(2), new Sub(new Var("v"), new Add(new Var("w"), new Var("z"))));
        Aexpr e3 = new Add(new Var("x"), new Add(new Var("y"), new Add(new Var("z"), new Var("v"))));

        System.out.println(e1);
        System.out.println(e2);
        System.out.println(e3);
    }
}

class CstI extends Aexpr {
    protected final int i;
    public CstI(int i) { this.i = i; }
    public String toString() { return "" + i; }
    public int eval(Map<String, Integer> env) { return i; }
    public Aexpr simplify() { return this; }
    public boolean equals(Object o) { return o instanceof CstI && ((CstI) o).i == i; }
    public int hashCode() { return i; }
}

class Var extends Aexpr {
    protected final String name;
    public Var(String name) { this.name = name; }
    public String toString() { return name; }
    public int eval(Map<String, Integer> env) { return env.get(name); }
    public Aexpr simplify() { return this; }
    public boolean equals(Object o) { return o instanceof Var && ((Var) o).name.equals(name); }
    public int hashCode() { return name.hashCode(); }
}

abstract class Binop extends Aexpr {
    protected final Aexpr e1, e2;
    protected Binop(Aexpr e1, Aexpr e2) { this.e1 = e1; this.e2 = e2; }
    protected abstract String oper();
    public String toString() { return "(" + e1 + " " + oper() + " " + e2 + ")"; }
    public boolean equals(Object o) {
        if (o == null || o.getClass() != this.getClass()) return false;
        Binop that = (Binop) o;
        return e1.equals(that.e1) && e2.equals(that.e2);
    }
    public int hashCode() { return 31 * e1.hashCode() + e2.hashCode(); }
}

class Add extends Binop {
    public Add(Aexpr e1, Aexpr e2) { super(e1, e2); }
    protected String oper() { return "+"; }
    public int eval(Map<String, Integer> env) { return e1.eval(env) + e2.eval(env); }
    public Aexpr simplify() {
        Aexpr s1 = e1.simplify(), s2 = e2.simplify();
        if (isCst(s1, 0)) return s2;
        if (isCst(s2, 0)) return s1;
        return new Add(s1, s2);
    }
}

class Mul extends Binop {
    public Mul(Aexpr e1, Aexpr e2) { super(e1, e2); }
    protected String oper() { return "*"; }
    public int eval(Map<String, Integer> env) { return e1.eval(env) * e2.eval(env); }
    public Aexpr simplify() {
        Aexpr s1 = e1.simplify(), s2 = e2.simplify();
        if (isCst(s1, 0) || isCst(s2, 0)) return new CstI(0);
        if (isCst(s1, 1)) return s2;
        if (isCst(s2, 1)) return s1;
        return new Mul(s1, s2);
    }
}

class Sub extends Binop {
    public Sub(Aexpr e1, Aexpr e2) { super(e1, e2); }
    protected String oper() { return "-"; }
    public int eval(Map<String, Integer> env) { return e1.eval(env) - e2.eval(env); }
    public Aexpr simplify() {
        Aexpr s1 = e1.simplify(), s2 = e2.simplify();
        if (isCst(s2, 0)) return s1;
        if (s1.equals(s2)) return new CstI(0);
        return new Sub(s1, s2);
    }
}